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

public partial class PP_kk_daftar : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable wilayah = new DataTable();
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    StudentWebService service = new StudentWebService();
    string level, userid;
    String status, JM_status, strSektorPekerjaan, strSektorPekerjaan1, strSektorPekerjaan2, strAnakSyarikat, strAnakSyarikat1, strAnakSyarikat2, strTarafJawatan, strTarafJawatan1, strTarafJawatan2;
    String seq_count;
    string cw_code;

    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button6);
        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                TextBox13.Attributes.Add("Readonly", "Readonly");
                wilahBind();
                Pelaburan();
                Hubungan();
                Pengenalan();
                NegriBind();
                Banknama();
                Tujuan();
                category();
                var samp = Request.Url.Query;
                if (samp != "")
                {
                    TXTNOKP.Text = service.Decrypt(HttpUtility.UrlDecode(Request.QueryString["edit"]));
                    load_permohon();
                    srch_Click();

                    Button1.Visible = false;
                    Button2.Visible = false;
                }
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }

    }

    void load_permohon()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select app_new_icno,app_applcn_no from jpa_application where app_new_icno='"+ TXTNOKP.Text + "' order by Created_date ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_permohon.DataSource = dt;
            dd_permohon.DataTextField = "app_applcn_no";
            dd_permohon.DataValueField = "app_applcn_no";
            dd_permohon.DataBind();
            //dd_permohon.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    void category()
    {
        SqlConnection con = new SqlConnection(conString);
        string com = "select Applicant_Name,Applicant_Code from Ref_Applicant_Category order by Applicant_Name";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddkatper.DataSource = dt;
        ddkatper.DataBind();
        ddkatper.DataTextField = "Applicant_Name";
        ddkatper.DataValueField = "Applicant_Code";
        ddkatper.DataBind();
        ddkatper.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        ddcat.DataSource = dt;
        ddcat.DataBind();
        ddcat.DataTextField = "Applicant_Name";
        ddcat.DataValueField = "Applicant_Code";
        ddcat.DataBind();
        ddcat.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        ddcat2.DataSource = dt;
        ddcat2.DataBind();
        ddcat2.DataTextField = "Applicant_Name";
        ddcat2.DataValueField = "Applicant_Code";
        ddcat2.DataBind();
        ddcat2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    }
    void Tujuan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select * from ref_tujuan WHERE Status = 'A' order by tujuan_desc ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            dd_tujuan.DataSource = dt;
            dd_tujuan.DataTextField = "tujuan_desc";
            dd_tujuan.DataValueField = "tujuan_cd";
            dd_tujuan.DataBind();
            dd_tujuan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Pelaburan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select * from Ref_Jenis_Pelaburan WHERE Status = 'A' order by Description ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Pelaburan.DataSource = dt;
            DD_Pelaburan.DataTextField = "Description";
            DD_Pelaburan.DataValueField = "Description_Code";
            DD_Pelaburan.DataBind();
            DD_Pelaburan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

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
           
            DD_Hubungan2.DataSource = dt;
            DD_Hubungan2.DataTextField = "Contact_Name";
            DD_Hubungan2.DataValueField = "Contact_Code";
            DD_Hubungan2.DataBind();
            DD_Hubungan2.Items.Insert(0, new ListItem("--- PILIH ---", ""));
            DD_Hubungan3.DataSource = dt;
            DD_Hubungan3.DataTextField = "Contact_Name";
            DD_Hubungan3.DataValueField = "Contact_Code";
            DD_Hubungan3.DataBind();
            DD_Hubungan3.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
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

    void NegriBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Decription,Decription_Code from Ref_Negeri  WHERE Status = 'A' order by Decription";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_NegriBind1.DataSource = dt;
            DD_NegriBind1.DataTextField = "Decription";
            DD_NegriBind1.DataValueField = "Decription_Code";
            DD_NegriBind1.DataBind();
            DD_NegriBind1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DD_NegriBind2.DataSource = dt;
            DD_NegriBind2.DataTextField = "Decription";
            DD_NegriBind2.DataValueField = "Decription_Code";
            DD_NegriBind2.DataBind();
            DD_NegriBind2.Items.Insert(0, new ListItem("--- PILIH ---", ""));

            DD_NegriBind3.DataSource = dt;
            DD_NegriBind3.DataTextField = "Decription";
            DD_NegriBind3.DataValueField = "Decription_Code";
            DD_NegriBind3.DataBind();
            DD_NegriBind3.Items.Insert(0, new ListItem("--- PILIH ---", ""));

          

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

    void Banknama()
    {
        string com = "select Bank_Name,Bank_Code from Ref_Nama_Bank WHERE Status = 'A' order by Bank_Name ASC";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        Bank_details.DataSource = dt;
        Bank_details.DataBind();
        Bank_details.DataTextField = "Bank_Name";
        Bank_details.DataValueField = "Bank_Code";
        Bank_details.DataBind();
        Bank_details.Items.Insert(0, new ListItem("--- PILIH ---", ""));
    }

    void wilahBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select Wilayah_Name,Wilayah_Code from Ref_Wilayah order by Wilayah_Name ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_wilayah.DataSource = dt;
            DD_wilayah.DataTextField = "Wilayah_Name";
            DD_wilayah.DataValueField = "Wilayah_Code";
            DD_wilayah.DataBind();
            DD_wilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void ddkaw_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    //-Pusat---------------------------------------------------------------------------------
    //    string cmd6 = "select distinct cawangan_code,cawangan_name from  Ref_Cawangan where wilayah_code='" + DD_wilayah.SelectedItem.Value + "'";
    //    con.Open();
    //    SqlDataAdapter adapterP = new SqlDataAdapter(cmd6, con);
    //    adapterP.Fill(wilayah);

    //    DD_cawangan.DataSource = wilayah;
    //    DD_cawangan.DataTextField = "cawangan_name";
    //    DD_cawangan.DataValueField = "cawangan_code";
    //    DD_cawangan.DataBind();
    //    con.Close();
    //    DD_cawangan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //}

    //void CawBind()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com1 = "select distinct cawangan_code,cawangan_name from  Ref_Cawangan Order by cawangan_code Asc";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com1, con);
    //        DataTable dt1 = new DataTable();
    //        adpt.Fill(dt1);
    //        DD_cawangan.DataSource = dt1;
    //        DD_cawangan.DataTextField = "cawangan_name";
    //        DD_cawangan.DataValueField = "cawangan_code";
    //        DD_cawangan.DataBind();
    //        DD_cawangan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    protected void bind_permohon(object sender, EventArgs e)
    {
       if(dd_permohon.SelectedValue != "")
        {
            srch_Click();
        }
    }
    void srch_Click()
    {
          try
        {
            if (TXTNOKP.Text != "")
            {

                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select JA.* from jpa_application as JA Left join mem_member as mm ON mm.mem_new_icno=JA.app_new_icno where JA.app_applcn_no='" + dd_permohon.SelectedValue + "'");
                if (ddicno.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Maklumat Carian Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                else
                {
                    //CawBind();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Permohonan Telah Diluluskan. Pindaan Maklumat Permohonan Tidak Dibenarkan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    string Select_App_query = "select ISNULL(mm.mem_centre,'') as mem_centre,ISNULL(mm.mem_member_no,'') as mem_member_no,mm.mem_applicant_type_cd,JA.* from jpa_application as JA Left join mem_member as mm ON mm.mem_new_icno = JA.app_new_icno where JA.app_applcn_no='" + dd_permohon.SelectedValue + "'";
                    con.Open();
                    var sqlCommand = new SqlCommand(Select_App_query, con);
                    var sqlReader = sqlCommand.ExecuteReader();
                    if (sqlReader.Read() == true)
                    {
                        // Maklumat Pemohon
                        Button6.Visible = true;
                        TextBox2.Text = sqlReader["app_name"].ToString();
                        TextBox13.Text = sqlReader["mem_member_no"].ToString();
                        var ic_count = TXTNOKP.Text.Length;
                        string str_age = string.Empty, c_age = string.Empty;
                        string c_dt1 = DateTime.Now.ToString("yyyy");
                        if (sqlReader["app_age"].ToString() == "")
                        {
                           

                            if (ic_count == 12)
                            {
                                str_age = "19" + TXTNOKP.Text.Substring(0, 2);
                                c_age = (double.Parse(c_dt1) - double.Parse(str_age)).ToString();
                            }
                            else
                            {
                                c_age = "";
                            }
                            app_age.Text = c_age;
                        }
                       
                        DD_Pelaburan.SelectedValue = sqlReader["app_loan_type_cd"].ToString();
                        dd_tujuan.SelectedValue = sqlReader["app_loan_purpose_cd"].ToString();
                        DD_wilayah.SelectedValue = sqlReader["app_region_cd"].ToString();
                        cw_code = sqlReader["app_region_cd"].ToString();
                        //if (sqlReader["app_branch_cd"].ToString() != "")
                        //{

                        //    DD_cawangan.SelectedValue = sqlReader["app_branch_cd"].ToString();
                        //}
                        //else
                        //{
                        //    DD_cawangan.SelectedValue = "";
                        //}
                        if (sqlReader["mem_applicant_type_cd"].ToString() != "")
                        {
                            ddkatper.SelectedValue = sqlReader["mem_applicant_type_cd"].ToString();
                            if (sqlReader["mem_applicant_type_cd"].ToString() == "LT")
                            {
                                Div3.Visible = false;
                                Div3_1.Visible = false;
                            }
                            else
                            {
                                Div3.Visible = true;
                                Div3_1.Visible = true;
                            }
                        }
                        else
                        {
                            ddkatper.SelectedValue = "";
                        }
                       
                        TextArea1.Value = sqlReader["app_permnt_address"].ToString();
                        TextArea4.Value = sqlReader["app_mailing_address"].ToString();
                        At_pcode.Text = sqlReader["app_permnt_postcode"].ToString();
                        As_pcode.Text = sqlReader["app_mailing_postcode"].ToString();
                        DD_NegriBind1.SelectedValue = sqlReader["app_permnt_state_cd"].ToString();
                        DD_NegriBind2.SelectedValue = sqlReader["app_mailing_state_cd"].ToString();
                        telefon_h.Text = sqlReader["app_phone_h"].ToString();
                        telefon_o.Text = sqlReader["app_phone_o"].ToString();
                        telefon_m.Text = sqlReader["app_phone_m"].ToString();
                        app_bank_acc_no.Text = sqlReader["app_bank_acc_no"].ToString();
                        Bank_details.SelectedValue = sqlReader["app_bank_cd"].ToString();

                        // Butiran Permohonan

                        decimal amt = (decimal)sqlReader["app_apply_amt"];
                        TextBox4.Text = amt.ToString("C").Replace("RM","").Replace("$", "");
                        //TextBox4.Text =sqlReader["app_apply_amt"].ToString();
                        TextBox12.Text = sqlReader["app_apply_dur"].ToString();

                        // Maklumat Pasangan

                        TextBox14.Text = sqlReader["app_spouse_name"].ToString();
                        TextBox16.Text = sqlReader["app_spouse_icno"].ToString();
                        TextArea2.Value = sqlReader["app_spouse_address"].ToString();
                        TextBox18.Text = sqlReader["app_spouse_phone"].ToString();
                        TextBox56.Text = sqlReader["app_spouse_postcode"].ToString();
                        DD_NegriBind3.SelectedValue = sqlReader["app_spouse_state_cd"].ToString();

                        // Maklumat Perniagaan Pemohon

                        if (sqlReader["app_biz_type_ind"].ToString() == "S")
                        {
                            //RadioButton1.Checked = true;
                        }
                        else if (sqlReader["app_biz_type_ind"].ToString() == "K")
                        {
                            //RadioButton2.Checked = true;
                        }
                        else
                        {
                            //RadioButton3.Checked = true;
                        }

                        

                    }
                    sqlReader.Close();

                    // Fetching Maklumat Waris (1)

                    string Select_relative1 = "select * from jpa_relative where rel_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' and rel_seq_no='1'";
                    var sqlCommand_rel1 = new SqlCommand(Select_relative1, con);
                    var sqlReader_rel1 = sqlCommand_rel1.ExecuteReader();
                    if (sqlReader_rel1.Read() == true)
                    {

                        TextBox19.Text = sqlReader_rel1["rel_name"].ToString();
                        TextBox21.Text = sqlReader_rel1["rel_new_icno"].ToString();
                        TextBox23.Text = sqlReader_rel1["rel_phone"].ToString();
                        DD_Hubungan2.SelectedValue = sqlReader_rel1["rel_relation_cd"].ToString();

                    }
                    sqlReader_rel1.Close();
                    string Select_relative2 = "select * from jpa_relative where rel_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' and rel_seq_no='2'";
                    var sqlCommand_rel2 = new SqlCommand(Select_relative2, con);
                    var sqlReader_rel2 = sqlCommand_rel2.ExecuteReader();
                    if (sqlReader_rel2.Read() == true)
                    {

                        TextBox24.Text = sqlReader_rel2["rel_name"].ToString();
                        TextBox25.Text = sqlReader_rel2["rel_new_icno"].ToString();
                        TextBox26.Text = sqlReader_rel2["rel_phone"].ToString();
                        DD_Hubungan3.SelectedValue = sqlReader_rel2["rel_relation_cd"].ToString();

                    }
                    sqlReader_rel2.Close();

                    //DataTable ddicno_kha = new DataTable();
                    //ddicno_kha = DBCon.Ora_Execute_table("select * from jpa_khairat  where tkh_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' ORDER BY tkh_seq_no ASC");
                    //if (ddicno_kha.Rows.Count >= 1)
                    //{

                    //    //1
                    //    TextBox17.Text = ddicno_kha.Rows[0]["tkh_name"].ToString();
                    //    app_age.Text = ddicno_kha.Rows[0]["tkh_age"].ToString();
                    //    TextBox15.Text = ddicno_kha.Rows[0]["tkh_new_icno"].ToString();
                    //    //2

                    //}
                    //if (ddicno_kha.Rows.Count >= 2)
                    //{
                    //    TextBox9.Text = ddicno_kha.Rows[1]["tkh_name"].ToString();
                    //    TextBox10.Text = ddicno_kha.Rows[1]["tkh_age"].ToString();
                    //    TextBox11.Text = ddicno_kha.Rows[1]["tkh_new_icno"].ToString();
                    //    DD_Hubungan1.SelectedValue = ddicno_kha.Rows[1]["tkh_relation_cd"].ToString();
                    //    DD_Pengenalan1.SelectedValue = ddicno_kha.Rows[1]["tkh_ic_type_cd"].ToString();
                    //}


                    DataTable ddicno_gua = new DataTable();
                    ddicno_gua = DBCon.Ora_Execute_table("select * from jpa_guarantor where gua_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' ORDER BY gua_seq_no ASC");
                    int count = ddicno_gua.Rows.Count;
                    if (count != 0)
                    {

                        TextBox27.Text = ddicno_gua.Rows[0]["gua_name"].ToString();
                        TextBox35.Text = ddicno_gua.Rows[0]["gua_phone"].ToString();
                        TextArea7.Value = ddicno_gua.Rows[0]["gua_permanent_address"].ToString();
                        TextArea8.Value = ddicno_gua.Rows[0]["gua_mailing_address"].ToString();
                        TextBox62.Text = ddicno_gua.Rows[0]["gua_permanent_postcode"].ToString();
                        TextBox63.Text = ddicno_gua.Rows[0]["gua_mailing_postcode"].ToString();
                        DD_NegriBind5.SelectedValue = ddicno_gua.Rows[0]["gua_permanent_state_cd"].ToString();
                        DD_NegriBind6.SelectedValue = ddicno_gua.Rows[0]["gua_mailing_state_cd"].ToString();
                        DD_Pengenalan2.SelectedValue = ddicno_gua.Rows[0]["gua_ic_type_cd"].ToString();
                        if (ddicno_gua.Rows[1]["gua_job_sector_ind"].ToString() != "")
                        {
                            ddcat.SelectedValue = ddicno_gua.Rows[1]["gua_job_sector_ind"].ToString();
                        }

                        else
                        {
                            ddcat.SelectedValue = "";
                        }


                        if (ddicno_gua.Rows[0]["gua_job_status_ind"].ToString() == "T")
                        {
                            RB11.Checked = true;
                        }
                        else
                        {
                            RB12.Checked = true;
                        }

                        TextBox28.Text = ddicno_gua.Rows[0]["gua_icno"].ToString();
                        TextBox29.Text = ddicno_gua.Rows[0]["gua_job"].ToString();
                        decimal net_amt = (decimal)ddicno_gua.Rows[0]["gua_nett_income"];
                        TextBox31.Text = net_amt.ToString("C").Replace("RM", "").Replace("$", "");
                        decimal gross_amt = (decimal)ddicno_gua.Rows[0]["gua_gross_income"];
                        TextBox33.Text = gross_amt.ToString("C").Replace("RM", "").Replace("$", "");
                        TextBox34.Text = ddicno_gua.Rows[0]["gua_employer_name"].ToString();
                        TextBox30.Text = ddicno_gua.Rows[0]["gua_employer_phone"].ToString();
                        TextBox32.Text = ddicno_gua.Rows[0]["gua_employer_phone_ext"].ToString();
                    }
                    //2
                    if (count > 1)
                    {

                        TextBox36.Text = ddicno_gua.Rows[1]["gua_name"].ToString();
                        TextBox37.Text = ddicno_gua.Rows[1]["gua_phone"].ToString();
                        TextArea5.Value = ddicno_gua.Rows[1]["gua_permanent_address"].ToString();
                        TextArea6.Value = ddicno_gua.Rows[1]["gua_mailing_address"].ToString();
                        TextBox60.Text = ddicno_gua.Rows[1]["gua_permanent_postcode"].ToString();
                        TextBox61.Text = ddicno_gua.Rows[1]["gua_mailing_postcode"].ToString();
                        DD_NegriBind7.SelectedValue = ddicno_gua.Rows[1]["gua_permanent_state_cd"].ToString();
                        DD_NegriBind8.SelectedValue = ddicno_gua.Rows[1]["gua_mailing_state_cd"].ToString();
                        DD_Pengenalan3.SelectedValue = ddicno_gua.Rows[1]["gua_ic_type_cd"].ToString();
                     
                        if (ddicno_gua.Rows[1]["gua_job_sector_ind"].ToString() != "")
                        {
                            ddcat2.SelectedValue = ddicno_gua.Rows[1]["gua_job_sector_ind"].ToString();
                        }
                        
                        else
                        {
                            ddcat2.SelectedValue = "";
                        }

                        if (ddicno_gua.Rows[1]["gua_job_status_ind"].ToString() == "T")
                        {
                            TJ21.Checked = true;
                        }
                        else
                        {
                            TJ22.Checked = true;
                        }

                        TextBox38.Text = ddicno_gua.Rows[1]["gua_icno"].ToString();
                        TextBox39.Text = ddicno_gua.Rows[1]["gua_job"].ToString();
                        decimal net_amt1 = (decimal)ddicno_gua.Rows[1]["gua_nett_income"];
                        TextBox40.Text = net_amt1.ToString("0.00");
                        decimal gross_amt1 = (decimal)ddicno_gua.Rows[1]["gua_gross_income"];
                        TextBox41.Text = gross_amt1.ToString("0.00");

                        TextBox42.Text = ddicno_gua.Rows[1]["gua_employer_name"].ToString();
                        TextBox43.Text = ddicno_gua.Rows[1]["gua_employer_phone"].ToString();
                        TextBox44.Text = ddicno_gua.Rows[1]["gua_employer_phone_ext"].ToString();
                    }

                    //3
                    //if (count > 2)
                    //{
                    //    TextBox45.Text = ddicno_gua.Rows[2]["gua_name"].ToString();
                    //    TextBox46.Text = ddicno_gua.Rows[2]["gua_phone"].ToString();
                    //    TextArea9.Value = ddicno_gua.Rows[2]["gua_permanent_address"].ToString();
                    //    TextArea10.Value = ddicno_gua.Rows[2]["gua_mailing_address"].ToString();
                    //    TextBox58.Text = ddicno_gua.Rows[2]["gua_permanent_postcode"].ToString();
                    //    TextBox59.Text = ddicno_gua.Rows[2]["gua_mailing_postcode"].ToString();
                    //    DD_NegriBind9.SelectedValue = ddicno_gua.Rows[2]["gua_permanent_state_cd"].ToString();
                    //    DD_NegriBind10.SelectedValue = ddicno_gua.Rows[2]["gua_mailing_state_cd"].ToString();
                    //    DD_Pengenalan4.SelectedValue = ddicno_gua.Rows[2]["gua_ic_type_cd"].ToString();
                       
                    //    if (ddicno_gua.Rows[2]["gua_job_sector_ind"].ToString() != "")
                    //    {
                    //        ddcat2.SelectedValue = ddicno_gua.Rows[2]["gua_job_sector_ind"].ToString();
                    //    }
                        
                    //    else
                    //    {
                    //        ddcat2.SelectedValue = "";
                    //    }

                    //    if (ddicno_gua.Rows[2]["gua_job_status_ind"].ToString() == "T")
                    //    {
                    //        TJ31.Checked = true;
                    //    }
                    //    else
                    //    {
                    //        TJ32.Checked = true;
                    //    }

                    //    TextBox47.Text = ddicno_gua.Rows[2]["gua_icno"].ToString();
                    //    TextBox48.Text = ddicno_gua.Rows[2]["gua_job"].ToString();
                    //    decimal net_amt2 = (decimal)ddicno_gua.Rows[2]["gua_nett_income"];
                    //    TextBox49.Text = net_amt2.ToString("0.00");
                    //    decimal gross_amt2 = (decimal)ddicno_gua.Rows[2]["gua_gross_income"];
                    //    TextBox50.Text = gross_amt2.ToString("0.00");

                    //    TextBox51.Text = ddicno_gua.Rows[2]["gua_employer_name"].ToString();
                    //    TextBox52.Text = ddicno_gua.Rows[2]["gua_employer_phone"].ToString();
                    //    TextBox53.Text = ddicno_gua.Rows[2]["gua_employer_phone_ext"].ToString();


                    //}

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai(Issue)',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }
    protected void Searchbtn_Click(object sender, EventArgs e)
    {
        srch_Click();
    }


    protected void update(object sender, EventArgs e)
    {
        try
        {
            DataTable ddicno1 = new DataTable();
            ddicno1 = DBCon.Ora_Execute_table("select app_new_icno from jpa_application where app_applcn_no='" + dd_permohon.SelectedValue + "'");
            if (ddicno1.Rows.Count != 0)
            {
                    status = "P";
                    JM_status = "R";
                

                SqlCommand insert_application = new SqlCommand("Update jpa_application set app_name='" + TextBox2.Text + "',app_age='"+ app_age.Text +"',app_loan_type_cd='" + DD_Pelaburan.SelectedValue + "',app_loan_purpose_cd='" + dd_tujuan.SelectedValue + "',app_region_cd='" + DD_wilayah.SelectedValue + "',app_apply_amt='" + TextBox4.Text + "',app_apply_dur='" + TextBox12.Text + "',app_permnt_address='" + TextArea1.Value.Replace("'", "''").Trim().ToUpper() + "',app_permnt_postcode='" + At_pcode.Text + "',app_permnt_state_cd='" + DD_NegriBind1.SelectedValue + "',app_mailing_address='" + TextArea4.Value.Replace("'", "''").ToUpper() + "',app_mailing_postcode='" + As_pcode.Text + "',app_mailing_state_cd='" + DD_NegriBind2.SelectedValue + "',app_phone_h='" + telefon_h.Text + "',app_phone_o='" + telefon_o.Text + "',app_phone_m='" + telefon_m.Text + "',app_bank_acc_no='" + app_bank_acc_no.Text.Trim() + "',app_bank_cd='" + Bank_details.SelectedValue + "',app_spouse_icno='" + TextBox16.Text + "',app_spouse_name='" + TextBox14.Text.Replace("'", "''") + "',app_spouse_phone='" + TextBox18.Text + "',app_spouse_address='" + TextArea2.Value.Replace("'", "''") + "',app_spouse_postcode='" + TextBox56.Text + "',app_spouse_state_cd='" + DD_NegriBind3.SelectedValue + "',app_biz_type_ind='" + status + "',app_biz_own_type_ind='" + JM_status + "',app_loan_amt='" + TextBox4.Text + "',appl_loan_dur='" + TextBox12.Text + "',app_upd_id='" + Session["New"].ToString() + "',app_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE app_applcn_no='" + dd_permohon.SelectedValue + "'", con);
                con.Open();
                int ia = insert_application.ExecuteNonQuery();
                con.Close();

                // Maklumat Waris Terdekat

                DataTable app_no = new DataTable();
                app_no = DBCon.Ora_Execute_table("select app_applcn_no from jpa_application where app_applcn_no='" + dd_permohon.SelectedValue + "'");

                if (TextBox19.Text != "")
                {
                    DataTable gua_seq_rel1 = new DataTable();
                    gua_seq_rel1 = DBCon.Ora_Execute_table("select rel_applcn_no from jpa_relative where rel_applcn_no='" + app_no.Rows[0]["app_applcn_no"] + "' and rel_seq_no = '1'");
                    if (gua_seq_rel1.Rows.Count != 0)
                    {
                        SqlCommand update_r1 = new SqlCommand("UPDATE jpa_relative SET rel_new_icno='" + TextBox21.Text + "',rel_name='" + TextBox19.Text.Trim().ToUpper().Replace("'", "''") + "',rel_phone='" + TextBox23.Text + "',rel_relation_cd='" + DD_Hubungan2.SelectedValue + "' where rel_applcn_no='" + app_no.Rows[0]["app_applcn_no"] + "' and rel_seq_no='1'", con);
                        con.Open();
                        int r1 = update_r1.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        DBCon.Execute_CommamdText("INSERT INTO jpa_relative (rel_applcn_no,rel_new_icno,rel_seq_no,rel_name,rel_phone,rel_relation_cd,created_date) values ('" + app_no.Rows[0]["app_applcn_no"] + "','" + TextBox21.Text + "','1','" + TextBox19.Text.Trim().ToUpper().Replace("'", "''") + "','" + TextBox23.Text + "','" + DD_Hubungan2.SelectedValue + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                    }
                }
                if (TextBox24.Text != "")
                {
                    DataTable gua_seq_rel2 = new DataTable();
                    gua_seq_rel2 = DBCon.Ora_Execute_table("select rel_applcn_no from jpa_relative where rel_applcn_no='" + app_no.Rows[0]["app_applcn_no"] + "' and rel_seq_no = '2'");
                    if (gua_seq_rel2.Rows.Count != 0)
                    {

                        SqlCommand update_r2 = new SqlCommand("UPDATE jpa_relative SET rel_new_icno='" + TextBox25.Text + "',rel_name='" + TextBox24.Text.Trim().ToUpper().Replace("'", "''") + "',rel_phone='" + TextBox26.Text + "',rel_relation_cd='" + DD_Hubungan3.SelectedValue + "' where rel_applcn_no='" + app_no.Rows[0]["app_applcn_no"] + "' and rel_seq_no='2'", con);
                        con.Open();
                        int r2 = update_r2.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {

                        DBCon.Execute_CommamdText("INSERT INTO jpa_relative (rel_applcn_no,rel_new_icno,rel_seq_no,rel_name,rel_phone,rel_relation_cd,created_date) values ('" + app_no.Rows[0]["app_applcn_no"] + "','" + TextBox25.Text + "','2','" + TextBox24.Text.Trim().ToUpper().Replace("'", "''") + "','" + TextBox26.Text + "','" + DD_Hubungan3.SelectedValue + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");

                    }
                }
                // Maklumat Tabung Khairat Hutang (Penama 2)

                //SqlCommand update_tkh1 = new SqlCommand("UPDATE jpa_khairat SET tkh_new_icno='" + TextBox15.Text.Trim().ToUpper() + "',tkh_name='" + TextBox17.Text.Trim().ToUpper().Replace("'", "''") + "',tkh_age='" + app_age.Text + "' where tkh_applcn_no='" + app_no.Rows[0]["app_applcn_no"] + "' and tkh_seq_no='1'", con);
                //con.Open();
                //int tkh1 = update_tkh1.ExecuteNonQuery();
                //con.Close();

                //if (TextBox9.Text != "")
                //{
                //    DataTable gua_seq_khai2 = new DataTable();
                //    gua_seq_khai2 = DBCon.Ora_Execute_table("select tkh_applcn_no from jpa_khairat where tkh_applcn_no='" + app_no.Rows[0]["app_applcn_no"] + "' and tkh_seq_no = '2'");
                //    if (gua_seq_khai2.Rows.Count != 0)
                //    {

                //        SqlCommand update_tkh2 = new SqlCommand("UPDATE jpa_khairat SET tkh_new_icno='" + TextBox11.Text.Trim().ToUpper() + "',tkh_name='" + TextBox9.Text.Trim().ToUpper().Replace("'", "''") + "',tkh_age='" + TextBox10.Text + "',tkh_ic_type_cd='" + DD_Pengenalan1.SelectedValue + "',tkh_relation_cd='" + DD_Hubungan1.SelectedValue + "' where tkh_applcn_no='" + app_no.Rows[0]["app_applcn_no"] + "' and tkh_seq_no='2'", con);
                //        con.Open();
                //        int tkh2 = update_tkh2.ExecuteNonQuery();
                //        con.Close();
                //    }
                //    else
                //    {

                //        DBCon.Execute_CommamdText("INSERT INTO jpa_khairat (tkh_applcn_no,tkh_new_icno,tkh_seq_no,tkh_name,tkh_age,tkh_ic_type_cd,tkh_relation_cd,created_date) values ('" + app_no.Rows[0]["app_applcn_no"] + "','" + TextBox11.Text.Trim().ToUpper() + "','2','" + TextBox9.Text.Trim().ToUpper().Replace("'", "''") + "','" + TextBox10.Text + "','" + DD_Pengenalan1.SelectedValue + "','" + DD_Hubungan1.SelectedValue + "','" + DateTime.Now + "')");


                //    }
                //}

                // Insert table on jpa_guarantor
                // 1

                if (ddcat.SelectedValue!="")
                {
                    strSektorPekerjaan = ddcat.SelectedValue;
                }
               
                else
                {
                    strSektorPekerjaan = "";

                }

              
                    strAnakSyarikat = "1";
               

                if (RB11.Checked == true)
                {
                    strTarafJawatan = "T";
                }
                else
                {
                    strTarafJawatan = "K";
                }


                SqlCommand update_gua1 = new SqlCommand("UPDATE jpa_guarantor SET gua_icno='" + TextBox28.Text + "',gua_ic_type_cd='" + DD_Pengenalan2.SelectedValue + "',gua_name='" + TextBox27.Text.Trim().ToUpper().Replace("'", "''") + "',gua_phone='" + TextBox35.Text + "',gua_permanent_address='" + TextArea7.Value.Replace("'", "''").ToUpper() + "',gua_permanent_postcode='" + TextBox62.Text + "',gua_permanent_state_cd='" + DD_NegriBind5.SelectedValue + "',gua_mailing_address='" + TextArea8.Value.Replace("'", "''").ToUpper() + "',gua_mailing_postcode='" + TextBox63.Text + "',gua_mailing_state_cd='" + DD_NegriBind6.SelectedValue + "',gua_job='" + TextBox29.Text + "',gua_job_sector_ind='" + strSektorPekerjaan + "',gua_job_subsidiary_ind='" + strAnakSyarikat + "',gua_job_status_ind='" + strTarafJawatan + "',gua_gross_income='" + TextBox33.Text + "',gua_nett_income='" + TextBox31.Text + "',gua_employer_name='" + TextBox34.Text.Trim().ToUpper().Replace("'", "''") + "',gua_employer_phone='" + TextBox30.Text + "',gua_employer_phone_ext='" + TextBox32.Text + "' where gua_applcn_no = '" + app_no.Rows[0]["app_applcn_no"] + "' and gua_seq_no='1'", con);
                con.Open();
                int gua1 = update_gua1.ExecuteNonQuery();
                con.Close();


                // 2

                if (DD_Pengenalan3.SelectedValue != "" && TextBox38.Text != "")
                {
                    DataTable gua_seq_check2 = new DataTable();
                    gua_seq_check2 = DBCon.Ora_Execute_table("select gua_applcn_no from jpa_guarantor where gua_applcn_no='" + app_no.Rows[0]["app_applcn_no"] + "' and gua_seq_no = '2'");
                    if (gua_seq_check2.Rows.Count != 0)
                    {
                      

                        if (TJ21.Checked == true)
                        {
                            strTarafJawatan1 = "T";
                        }
                        else if (TJ22.Checked == true)
                        {
                            strTarafJawatan1 = "K";
                        }
                        else
                        {
                            strTarafJawatan1 = "";
                        }

                        SqlCommand update_gua2 = new SqlCommand("UPDATE jpa_guarantor SET gua_icno='" + TextBox38.Text + "',gua_ic_type_cd='" + DD_Pengenalan3.SelectedValue + "',gua_name='" + TextBox36.Text.Trim().ToUpper().Replace("'", "''") + "',gua_phone='" + TextBox37.Text + "',gua_permanent_address='" + TextArea5.Value.Replace("'", "''").ToUpper() + "',gua_permanent_postcode='" + TextBox60.Text + "',gua_permanent_state_cd='" + DD_NegriBind7.SelectedValue + "',gua_mailing_address='" + TextArea6.Value.Replace("'", "''").ToUpper() + "',gua_mailing_postcode='" + TextBox61.Text + "',gua_mailing_state_cd='" + DD_NegriBind8.SelectedValue + "',gua_job='" + TextBox39.Text + "',gua_job_sector_ind='" + strSektorPekerjaan1 + "',gua_job_subsidiary_ind='" + strAnakSyarikat1 + "',gua_job_status_ind='" + strTarafJawatan1 + "',gua_gross_income='" + TextBox41.Text + "',gua_nett_income='" + TextBox40.Text + "',gua_employer_name='" + TextBox42.Text.Trim().ToUpper().Replace("'", "''") + "',gua_employer_phone='" + TextBox43.Text + "',gua_employer_phone_ext='" + TextBox44.Text + "' where gua_applcn_no = '" + app_no.Rows[0]["app_applcn_no"] + "' and gua_seq_no='2'", con);
                        con.Open();
                        int gua2 = update_gua2.ExecuteNonQuery();
                        con.Close();


                    }
                    else
                    {
                        SqlCommand insert_gua2 = new SqlCommand("INSERT INTO jpa_guarantor (gua_applcn_no,gua_icno,gua_ic_type_cd,gua_name,gua_phone,gua_permanent_address,gua_permanent_postcode,gua_permanent_state_cd,gua_mailing_address,gua_mailing_postcode,gua_mailing_state_cd,gua_job,gua_job_sector_ind,gua_job_subsidiary_ind,gua_job_status_ind,gua_gross_income,gua_nett_income,gua_employer_name,gua_employer_phone,gua_employer_phone_ext,Created_date,gua_seq_no) VALUES ('" + app_no.Rows[0]["app_applcn_no"] + "','" + TextBox38.Text + "','" + DD_Pengenalan3.SelectedValue + "','" + TextBox36.Text.Trim().ToUpper().Replace("'", "''") + "','" + TextBox37.Text + "','" + TextArea5.Value.Replace("'", "''").ToUpper() + "','" + TextBox60.Text + "','" + DD_NegriBind7.SelectedValue + "','" + TextArea6.Value.Replace("'", "''").ToUpper() + "','" + TextBox61.Text + "','" + DD_NegriBind8.SelectedValue + "','" + TextBox39.Text + "','" + strSektorPekerjaan1 + "','" + strAnakSyarikat1 + "','" + strTarafJawatan1 + "','" + TextBox41.Text + "','" + TextBox40.Text + "','" + TextBox42.Text.Trim().ToUpper().Replace("'", "''") + "','" + TextBox43.Text + "','" + TextBox44.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','2')", con);
                        con.Open();
                        int ins_gua2 = insert_gua2.ExecuteNonQuery();
                        con.Close();
                    }
                }
                // 3
                //if (DD_Pengenalan4.SelectedValue != "" && TextBox47.Text != "")
                //{
                //    DataTable gua_seq_check3 = new DataTable();
                //    gua_seq_check3 = DBCon.Ora_Execute_table("select gua_applcn_no from jpa_guarantor where gua_applcn_no='" + app_no.Rows[0]["app_applcn_no"] + "' and gua_seq_no = '3'");
                //    if (gua_seq_check3.Rows.Count != 0)
                //    {

                        

                //        if (TJ31.Checked == true)
                //        {
                //            strTarafJawatan2 = "T";
                //        }
                //        else if (TJ32.Checked == true)
                //        {
                //            strTarafJawatan2 = "K";
                //        }
                //        else
                //        {
                //            strTarafJawatan2 = "";
                //        }
                //        SqlCommand update_gua3 = new SqlCommand("UPDATE jpa_guarantor SET gua_icno='" + TextBox47.Text + "',gua_ic_type_cd='" + DD_Pengenalan4.SelectedValue + "',gua_name='" + TextBox45.Text.Trim().ToUpper() + "',gua_phone='" + TextBox46.Text + "',gua_permanent_address='" + TextArea9.Value.Replace("'", "''").ToUpper() + "',gua_permanent_postcode='" + TextBox58.Text + "',gua_permanent_state_cd='" + DD_NegriBind9.SelectedValue + "',gua_mailing_address='" + TextArea10.Value.Replace("'", "''").ToUpper() + "',gua_mailing_postcode='" + TextBox59.Text + "',gua_mailing_state_cd='" + DD_NegriBind10.SelectedValue + "',gua_job='" + TextBox48.Text + "',gua_job_sector_ind='" + strSektorPekerjaan2 + "',gua_job_subsidiary_ind='" + strAnakSyarikat2 + "',gua_job_status_ind='" + strTarafJawatan2 + "',gua_gross_income='" + TextBox50.Text + "',gua_nett_income='" + TextBox49.Text + "',gua_employer_name='" + TextBox51.Text.Trim().ToUpper() + "',gua_employer_phone='" + TextBox52.Text + "',gua_employer_phone_ext='" + TextBox53.Text + "' where gua_applcn_no = '" + app_no.Rows[0]["app_applcn_no"] + "' and gua_seq_no='3'", con);
                //        con.Open();
                //        int gua3 = update_gua3.ExecuteNonQuery();
                //        con.Close();


                //    }
                //    else
                //    {
                //        SqlCommand insert_gua3 = new SqlCommand("INSERT INTO jpa_guarantor (gua_applcn_no,gua_icno,gua_ic_type_cd,gua_name,gua_phone,gua_permanent_address,gua_permanent_postcode,gua_permanent_state_cd,gua_mailing_address,gua_mailing_postcode,gua_mailing_state_cd,gua_job,gua_job_sector_ind,gua_job_subsidiary_ind,gua_job_status_ind,gua_gross_income,gua_nett_income,gua_employer_name,gua_employer_phone,gua_employer_phone_ext,Created_date,gua_seq_no) VALUES ('" + app_no.Rows[0]["app_applcn_no"] + "','" + TextBox47.Text + "','" + DD_Pengenalan4.SelectedValue + "','" + TextBox45.Text.Trim().ToUpper() + "','" + TextBox46.Text + "','" + TextArea9.Value.Replace("'", "''").ToUpper() + "','" + TextBox58.Text + "','" + DD_NegriBind9.SelectedValue + "','" + TextArea10.Value.Replace("'", "''").ToUpper() + "','" + TextBox59.Text + "','" + DD_NegriBind10.SelectedValue + "','" + TextBox48.Text + "','" + strSektorPekerjaan2 + "','" + strAnakSyarikat2 + "','" + strTarafJawatan2 + "','" + TextBox50.Text + "','" + TextBox49.Text + "','" + TextBox51.Text.Trim().ToUpper() + "','" + TextBox52.Text + "','" + TextBox53.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','3')", con);
                //        con.Open();
                //        int ins_gua3 = insert_gua3.ExecuteNonQuery();
                //        con.Close();
                //    }
                //}
               
                //Button3.Visible = false;
                //Button6.Visible = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Dikemaskini',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Ic No wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }

    }



    protected void Rdbwar_CheckedChanged(object sender, EventArgs e)
    {
      
    }
    protected void RdbBwar_CheckedChanged(object sender, EventArgs e)
    {
        
    }
    protected void RdbPT_CheckedChanged(object sender, EventArgs e)
    {
        
    }

    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
       
    }

    protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
    {
       
    }


   

    void tab1()
    {
        p5.Attributes.Add("class", "tab-pane active");
        p6.Attributes.Add("class", "tab-pane");
     

        Li3.Attributes.Add("class", "active");
        Li4.Attributes.Remove("class");
    

    }

    void tab2()
    {
        p6.Attributes.Add("class", "tab-pane active");
        p5.Attributes.Add("class", "tab-pane");
      

        Li4.Attributes.Add("class", "active");
        Li3.Attributes.Remove("class");
      

    }

    void tab3()
    {
        
        p6.Attributes.Add("class", "tab-pane");
        p5.Attributes.Add("class", "tab-pane");

        Li4.Attributes.Remove("class");
        Li3.Attributes.Remove("class");

    }

    protected void RB1_CheckedChanged(object sender, EventArgs e)
    {
        
        tab1();

    }

    protected void RB1_1_CheckedChanged(object sender, EventArgs e)
    {
       
        tab2();
    }

    protected void RB2_1_CheckedChanged(object sender, EventArgs e)
    {
        
        tab3();
    }
    protected void RB2_CheckedChanged(object sender, EventArgs e)
    {
        tab1();
    }

    protected void RB1_2_CheckedChanged(object sender, EventArgs e)
    {
       
        
        tab2();
    }

    protected void RB2_2_CheckedChanged(object sender, EventArgs e)
    {
      
        tab3();

    }
    protected void RB3_CheckedChanged(object sender, EventArgs e)
    {
       
        tab1();
        
    }

    protected void RB1_3_CheckedChanged(object sender, EventArgs e)
    {
      
        tab2();

    }

    protected void RB2_3_CheckedChanged(object sender, EventArgs e)
    {
       
        tab3();
    }

    protected void RB4_CheckedChanged(object sender, EventArgs e)
    {
      
        tab1();
    }

    protected void RB1_4_CheckedChanged(object sender, EventArgs e)
    {
      
        tab2();
    }
    protected void RB2_4_CheckedChanged(object sender, EventArgs e)
    {
       
        tab3();
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
        
       
        tab3();
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
       
     
        tab3();
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }


    protected void click_Reset(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void clk_print(object sender, EventArgs e)
    {
        {
            try
            {

                if (TXTNOKP.Text != "")
                {
                    string v1 = string.Empty, v2 = string.Empty, v3 = string.Empty, v4 = string.Empty, v5 = string.Empty, v6 = string.Empty, v7 = string.Empty, v8 = string.Empty, v9 = string.Empty, v10 = string.Empty;
                    string v11 = string.Empty, v12 = string.Empty, v13 = string.Empty, v14 = string.Empty, v15 = string.Empty, v16 = string.Empty, v17 = string.Empty, v18 = string.Empty, v19 = string.Empty, v20 = string.Empty;
                    string v21 = string.Empty, v22 = string.Empty, v23 = string.Empty, v24 = string.Empty, v25 = string.Empty, v26 = string.Empty, v27 = string.Empty, v28 = string.Empty, v29 = string.Empty, v30 = string.Empty;
                    string v31 = string.Empty, v32 = string.Empty, v33 = string.Empty, v34 = string.Empty, v35 = string.Empty, v36 = string.Empty, v37 = string.Empty, v38 = string.Empty, v39 = string.Empty, v40 = string.Empty, v41 = string.Empty, v42 = string.Empty, v43 = string.Empty, v44 = string.Empty, v45 = string.Empty, v46 = string.Empty, v47 = string.Empty, v48 = string.Empty, v49 = string.Empty, v50 = string.Empty;

                    string mv31 = string.Empty, mv32 = string.Empty, mv33 = string.Empty, mv34 = string.Empty, mv35 = string.Empty, mv36 = string.Empty, mv37 = string.Empty, mv38 = string.Empty, mv39 = string.Empty, mv40 = string.Empty, mv41 = string.Empty, mv42 = string.Empty, mv43 = string.Empty, mv44 = string.Empty, mv45 = string.Empty, mv46 = string.Empty, mv47 = string.Empty, mv48 = string.Empty, mv49 = string.Empty, mv50 = string.Empty;

                    string mv131 = string.Empty, mv132 = string.Empty, mv133 = string.Empty, mv134 = string.Empty, mv135 = string.Empty, mv136 = string.Empty, mv137 = string.Empty, mv138 = string.Empty, mv139 = string.Empty, mv140 = string.Empty, mv141 = string.Empty, mv142 = string.Empty, mv143 = string.Empty, mv144 = string.Empty, mv145 = string.Empty, mv146 = string.Empty, mv147 = string.Empty, mv148 = string.Empty, mv149 = string.Empty, mv150 = string.Empty;

                    string mv231 = string.Empty, mv232 = string.Empty, mv233 = string.Empty, mv234 = string.Empty, mv235 = string.Empty, mv236 = string.Empty, mv237 = string.Empty, mv238 = string.Empty, mv239 = string.Empty, mv240 = string.Empty, mv241 = string.Empty, mv242 = string.Empty, mv243 = string.Empty, mv244 = string.Empty, mv245 = string.Empty, mv246 = string.Empty, mv247 = string.Empty, mv248 = string.Empty, mv249 = string.Empty, mv250 = string.Empty;
                    //Path
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    //dt = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,RB.branch_desc,RT.tujuan_desc,RJ1.Jawatan_desc as desc1,RJ2.Jawatan_desc as desc2,RJ3.Jawatan_desc as desc3,JJA.* from jpa_application as JA Left join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no left join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_branch as RB ON RB.branch_cd=JA.app_branch_cd left join ref_tujuan as RT ON RT.tujuan_cd = JA.app_loan_purpose_cd left join ref_jawatan as RJ1 on RJ1.Jawatan_Code=JJA.jkk_post1 left join ref_jawatan as RJ2 on RJ2.Jawatan_Code=JJA.jkk_post2 left join ref_jawatan as RJ3 on RJ3.Jawatan_Code=JJA.jkk_post3 where JA.app_new_icno='" + Icno.Text + "'");
                    dt = DBCon.Ora_Execute_table("select JA.app_applcn_no,JA.app_new_icno,JA.app_name,JA.app_apply_amt,JA.app_apply_dur,RW.Wilayah_Name,RB.branch_desc,RT.tujuan_desc,RJ1.Jawatan_desc as desc1,RJ2.Jawatan_desc as desc2,RJ3.Jawatan_desc as desc3,JJA.*,case when jja.jkk_result_ind = 'L' then 'LULUS' when JJA.jkk_result_ind = 'B' then 'LULUS BERSYARAT' else 'TOLAK' end as result from jpa_application as JA Left join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no left join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_branch as RB ON RB.branch_cd=JA.app_branch_cd left join ref_tujuan as RT ON RT.tujuan_cd = JA.app_loan_purpose_cd left join ref_jawatan as RJ1 on RJ1.Jawatan_Code=JJA.jkk_post1 left join ref_jawatan as RJ2 on RJ2.Jawatan_Code=JJA.jkk_post2 left join ref_jawatan as RJ3 on RJ3.Jawatan_Code=JJA.jkk_post3 where JA.app_new_icno=''");
                    DataTable ddicno = new DataTable();
                    ddicno = DBCon.Ora_Execute_table("select app_new_icno,app_applcn_no from jpa_application where app_new_icno='" + TXTNOKP.Text + "'");

                    Rptviwer_kelulusan.Reset();
                    ds.Tables.Add(dt);

                    Rptviwer_kelulusan.LocalReport.DataSources.Clear();
                    //1
                    if (ddicno.Rows.Count != 0)
                    {
                        v50 = ddicno.Rows[0]["app_applcn_no"].ToString();
                    }
                    else
                    {
                        v50 = "";
                    }

                    v1 = TXTNOKP.Text;
                    v2 = TextBox2.Text;
                    v3 = TextBox13.Text;
                    if (DD_Pelaburan.SelectedValue != "")
                    {
                        v4 = DD_Pelaburan.SelectedItem.Text;
                    }
                    else
                    {
                        v4 = "";
                    }
                    if (dd_tujuan.SelectedValue != "")
                    {
                        v5 = dd_tujuan.SelectedItem.Text;
                    }
                    else
                    {
                        v5 = "";
                    }
                    if (DD_wilayah.SelectedValue != "")
                    {
                        v6 = DD_wilayah.SelectedItem.Text;
                    }
                    else
                    {
                        v6 = "";
                    }
                    //if (DD_cawangan.SelectedValue != "")
                    //{
                    //    v7 = DD_cawangan.SelectedItem.Text;
                    //}
                    //else
                    //{
                    //    v7 = "";
                    //}
                    if (ddkatper.SelectedValue != "")
                    {
                        v7 = ddkatper.SelectedItem.Text;
                    }
                    else
                    {
                        v7 = "";
                    }
                    v8 ="";
                    v9 = TextArea1.Value;
                    v10 = TextArea4.Value;
                    v11 = At_pcode.Text;
                    v12 = As_pcode.Text;
                    if (DD_NegriBind1.SelectedValue != "")
                    {
                        v13 = DD_NegriBind1.SelectedItem.Text;
                    }
                    else
                    {
                        v13 = "";
                    }
                    if (DD_NegriBind2.SelectedValue != "")
                    {
                        v14 = DD_NegriBind2.SelectedItem.Text;
                    }
                    else
                    {
                        v14 = "";
                    }
                    v15 = telefon_h.Text;
                    v16 = telefon_o.Text;
                    v17 = telefon_m.Text;
                    v18 = app_bank_acc_no.Text;
                    if (Bank_details.SelectedValue != "")
                    {
                        v19 = Bank_details.SelectedItem.Text;
                    }
                    else
                    {
                        v19 = "";
                    }

                    //2

                    v20 = TextBox4.Text;
                    v21 = TextBox12.Text;

                    //3-1
                  
                    //3-2
                   
                   
                        v28 = "";
                    
                   
                        v29 = "";
                    

                    //4

                    v30 = TextBox14.Text;
                    v31 = TextBox16.Text;
                    v32 = TextArea2.Value;
                    v33 = TextBox18.Text;
                    v34 = TextBox56.Text;
                    if (DD_NegriBind3.SelectedValue != "")
                    {
                        v35 = DD_NegriBind3.SelectedItem.Text;
                    }
                    else
                    {
                        v35 = "";
                    }

                    //5-1

                    v36 = TextBox19.Text;
                    v37 = TextBox21.Text;
                    v38 = TextBox23.Text;
                    if (DD_Hubungan2.SelectedValue != "")
                    {
                        v39 = DD_Hubungan2.SelectedItem.Text;
                    }
                    else
                    {
                        v39 = "";
                    }

                    //5-2

                    v40 = TextBox24.Text;
                    v41 = TextBox25.Text;
                    v42 = TextBox26.Text;
                    if (DD_Hubungan3.SelectedValue != "")
                    {
                        v43 = DD_Hubungan3.SelectedItem.Text;
                    }
                    else
                    {
                        v43 = "";
                    }

                  
                        v44 = "";
                    

                        v45 = "";
                    

                   
                        v48 = "";
                    

                    //mp-1

                    mv31 = TextBox27.Text;
                    mv32 = TextBox35.Text;
                    mv33 = TextArea7.Value;
                    mv34 = TextArea8.Value;
                    mv35 = TextBox62.Text;
                    mv36 = TextBox63.Text;
                    if (DD_NegriBind5.SelectedValue != "")
                    {
                        mv37 = DD_NegriBind5.SelectedItem.Text;
                    }
                    else
                    {
                        mv37 = "";
                    }
                    if (DD_NegriBind6.SelectedValue != "")
                    {
                        mv38 = DD_NegriBind6.SelectedItem.Text;
                    }
                    else
                    {
                        mv38 = "";
                    }
                    if (DD_Pengenalan2.SelectedValue != "")
                    {
                        mv39 = DD_Pengenalan2.SelectedItem.Text;
                    }
                    else
                    {
                        mv39 = "";
                    }
                    mv40 = TextBox28.Text;
                    mv41 = TextBox29.Text;
                    if (ddcat.SelectedItem.Text!="")
                    {
                        mv42 = ddcat.SelectedItem.Text;
                    }
                   
                    else
                    {
                        mv42 = "";
                    }

                    if (RB11.Checked == true)
                    {
                        mv43 = "Tetap";
                    }
                    else if (RB12.Checked == true)
                    {
                        mv43 = "Kontrak";
                    }
                    else
                    {
                        mv43 = "";
                    }
                    if (TextBox31.Text != "")
                    {
                        mv44 = double.Parse(TextBox31.Text).ToString("C").Replace("$", "").Replace("RM", "");
                    }
                    if (TextBox33.Text != "")
                    {
                        mv45 = double.Parse(TextBox33.Text).ToString("C").Replace("$", "").Replace("RM", "");
                    }
                 
                    mv46 = TextBox34.Text;
                    mv47 = TextBox30.Text;
                    mv48 = TextBox32.Text;

                    //mp-2

                    mv131 = TextBox36.Text;
                    mv132 = TextBox37.Text;
                    mv133 = TextArea5.Value;
                    mv134 = TextArea6.Value;
                    mv135 = TextBox60.Text;
                    mv136 = TextBox61.Text;
                    if (DD_NegriBind7.SelectedValue != "")
                    {
                        mv137 = DD_NegriBind7.SelectedItem.Text;
                    }
                    else
                    {
                        mv137 = "";
                    }
                    if (DD_NegriBind8.SelectedValue != "")
                    {
                        mv138 = DD_NegriBind8.SelectedItem.Text;
                    }
                    else
                    {
                        mv138 = "";
                    }
                    if (DD_Pengenalan3.SelectedValue != "")
                    {
                        mv139 = DD_Pengenalan3.SelectedItem.Text;
                    }
                    else
                    {
                        mv139 = "";
                    }
                    mv140 = TextBox38.Text;
                    mv141 = TextBox39.Text;
                    if (ddcat.SelectedValue!= "")
                    {
                        mv142 = ddcat.SelectedItem.Text;
                    }
                   
                    else
                    {
                        mv142 = "";
                    }

                    if (TJ21.Checked == true)
                    {
                        mv143 = "Tetap";
                    }
                    else if (TJ22.Checked == true)
                    {
                        mv143 = "Kontrak";
                    }
                    else
                    {
                        mv143 = "";
                    }

                    if (TextBox40.Text != "")
                    {
                        mv144 = double.Parse(TextBox40.Text).ToString("C").Replace("$", "").Replace("RM", "");
                    }
                    if (TextBox41.Text != "")
                    {
                        mv145 = double.Parse(TextBox41.Text).ToString("C").Replace("$", "").Replace("RM", "");
                    }
                    mv146 = TextBox42.Text;
                    mv147 = TextBox43.Text;
                    mv148 = TextBox44.Text;

                    //mp-3

                    //mv231 = TextBox45.Text;
                    //mv232 = TextBox46.Text;
                    //mv233 = TextArea9.Value;
                    //mv234 = TextArea10.Value;
                    //mv235 = TextBox58.Text;
                    //mv236 = TextBox59.Text;
                    //if (DD_NegriBind9.SelectedValue != "")
                    //{
                    //    mv237 = DD_NegriBind9.SelectedItem.Text;
                    //}
                    //else
                    //{
                    //    mv237 = "";
                    //}
                    //if (DD_NegriBind10.SelectedValue != "")
                    //{
                    //    mv238 = DD_NegriBind10.SelectedItem.Text;
                    //}
                    //else
                    //{
                    //    mv238 = "";
                    //}
                    //if (DD_Pengenalan4.SelectedValue != "")
                    //{
                    //    mv239 = DD_Pengenalan4.SelectedItem.Text;
                    //}
                    //else
                    //{
                    //    mv239 = "";
                    //}
                    //mv240 = TextBox47.Text;
                    //mv241 = TextBox48.Text;
                    //if (ddcat2.SelectedValue != "")
                    //{
                    //    mv242 = ddcat2.SelectedItem.Text;
                    //}

                    //else
                    //{
                    //    mv242 = "";
                    //}

                    //if (TJ31.Checked == true)
                    //{
                    //    mv243 = "Tetap";
                    //}
                    //else if (TJ32.Checked == true)
                    //{
                    //    mv243 = "Kontrak";
                    //}
                    //else
                    //{
                    //    mv243 = "";
                    //}


                    if (ddkatper.SelectedValue != "LT")
                    {
                        Rptviwer_kelulusan.LocalReport.ReportPath = "Pelaburan_Anggota/ppp.rdlc";
                    }
                    else
                    {
                        Rptviwer_kelulusan.LocalReport.ReportPath = "Pelaburan_Anggota/ppp1.rdlc";
                    }
                    ReportDataSource rds = new ReportDataSource("pp_ppp", dt);
                        ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("pp_v50",v50),
                      new ReportParameter("pp_v1",v1),
                       new ReportParameter("pp_v2",v2),
                        new ReportParameter("pp_v3",v3),
                         new ReportParameter("pp_v4",v4),
                          new ReportParameter("pp_v5",v5),
                           new ReportParameter("pp_v6",v6),
                            new ReportParameter("pp_v7",v7),
                             new ReportParameter("pp_v8",v8),
                              new ReportParameter("pp_v9",v9),
                               new ReportParameter("pp_v10",v10),
                                new ReportParameter("pp_v11",v11),
                                 new ReportParameter("pp_v12",v12),
                                  new ReportParameter("pp_v13",v13),
                                   new ReportParameter("pp_v14",v14),
                                    new ReportParameter("pp_v15",v15),
                                  new ReportParameter("pp_v16",v16),
                                  new ReportParameter("pp_v17",v17),
                                  new ReportParameter("pp_v18",v18),
                                  new ReportParameter("pp_v19",v19),

                                 new ReportParameter("pp_v20", double.Parse(v20).ToString("C").Replace("$","").Replace("RM","")),
                                  new ReportParameter("pp_v21",v21),
                                  new ReportParameter("pp_v22",v22),
                                  new ReportParameter("pp_v23",v23),
                                  new ReportParameter("pp_v24",v24),
                                  new ReportParameter("pp_v25",v25),
                                  new ReportParameter("pp_v26",v26),
                                  new ReportParameter("pp_v27",v27),
                                  new ReportParameter("pp_v28",v28),
                                  new ReportParameter("pp_v29",v29),

                                  new ReportParameter("pp_v30",v30),
                                  new ReportParameter("pp_v31",v31),
                                  new ReportParameter("pp_v32",v32),
                                  new ReportParameter("pp_v33",v33),
                                  new ReportParameter("pp_v34",v34),
                                  new ReportParameter("pp_v35",v35),

                                  new ReportParameter("pp_v36",v36),
                                  new ReportParameter("pp_v37",v37),
                                  new ReportParameter("pp_v38",v38),
                                  new ReportParameter("pp_v39",v39),
                                  new ReportParameter("pp_v40",v40),
                                  new ReportParameter("pp_v41",v41),
                                  new ReportParameter("pp_v42",v42),
                                  new ReportParameter("pp_v43",v43),

                                 new ReportParameter("pp_v44",v44),
                                  new ReportParameter("pp_v45",v45),
                                  new ReportParameter("pp_v46",v46),
                                  new ReportParameter("pp_v47",v47),
                                  new ReportParameter("pp_v48",v48),

                                  new ReportParameter("pp_mv31",mv31),
                                  new ReportParameter("pp_mv32",mv32),
                                  new ReportParameter("pp_mv33",mv33),
                                  new ReportParameter("pp_mv34",mv34),
                                  new ReportParameter("pp_mv35",mv35),
                                  new ReportParameter("pp_mv36",mv36),
                                  new ReportParameter("pp_mv37",mv37),
                                  new ReportParameter("pp_mv38",mv38),
                                  new ReportParameter("pp_mv39",mv39),
                                  new ReportParameter("pp_mv40",mv40),
                                  new ReportParameter("pp_mv41",mv41),
                                  new ReportParameter("pp_mv42",mv42),
                                  new ReportParameter("pp_mv43",mv43),
                                  new ReportParameter("pp_mv44",mv44),
                                  new ReportParameter("pp_mv45",mv45),
                                  new ReportParameter("pp_mv46",mv46),
                                  new ReportParameter("pp_mv47",mv47),
                                  new ReportParameter("pp_mv48",mv48),

                                  new ReportParameter("pp_mv131",mv131),
                                  new ReportParameter("pp_mv132",mv132),
                                  new ReportParameter("pp_mv133",mv133),
                                  new ReportParameter("pp_mv134",mv134),
                                  new ReportParameter("pp_mv135",mv135),
                                  new ReportParameter("pp_mv136",mv136),
                                  new ReportParameter("pp_mv137",mv137),
                                  new ReportParameter("pp_mv138",mv138),
                                  new ReportParameter("pp_mv139",mv139),
                                  new ReportParameter("pp_mv140",mv140),
                                  new ReportParameter("pp_mv141",mv141),
                                  new ReportParameter("pp_mv142",mv142),
                                  new ReportParameter("pp_mv143",mv143),
                                  new ReportParameter("pp_mv144",mv144),
                                  new ReportParameter("pp_mv145",mv145),
                                  new ReportParameter("pp_mv146",mv146),
                                  new ReportParameter("pp_mv147",mv147),
                                  new ReportParameter("pp_mv148",mv148),

                                  new ReportParameter("pp_mv231",mv231),
                                  new ReportParameter("pp_mv232",mv232),
                                  new ReportParameter("pp_mv233",mv233),
                                  new ReportParameter("pp_mv234",mv234),
                                  new ReportParameter("pp_mv235",mv235),
                                  new ReportParameter("pp_mv236",mv236),
                                  new ReportParameter("pp_mv237",mv237),
                                  new ReportParameter("pp_mv238",mv238),
                                  new ReportParameter("pp_mv239",mv239),
                                  new ReportParameter("pp_mv240",mv240),
                                  new ReportParameter("pp_mv241",mv241),
                                  new ReportParameter("pp_mv242",mv242),
                                  new ReportParameter("pp_mv243",mv243),
                                  new ReportParameter("pp_mv244",mv244),
                                  new ReportParameter("pp_mv245",mv245),
                                  new ReportParameter("pp_mv246",mv246),
                                  new ReportParameter("pp_mv247",mv247),
                                  new ReportParameter("pp_mv248",mv248),
                                   new ReportParameter("projek",""),


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


                        Response.AddHeader("content-disposition", "attachment; filename=KEMASKINI_PENDAFTARAN_PERMOHONAN_PEMBIAYAAN_" + TXTNOKP.Text + "." + extension);

                        Response.BinaryWrite(bytes);

                        //Response.Write("<script>");
                        //Response.Write("window.open('', '_newtab');");
                        //Response.Write("</script>");
                        Response.Flush();

                        Response.End();
                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No KP Baru',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    protected void clk_bak(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Pelaburan_Anggota/PP_kk_daftar_view.aspx");
    }
}