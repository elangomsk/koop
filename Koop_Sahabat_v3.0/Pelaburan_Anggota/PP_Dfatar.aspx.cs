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


public partial class PP_Dfatar : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    DataTable wilayah = new DataTable();
    string level, userid;
    String status, JM_status, strSektorPekerjaan, strSektorPekerjaan1, strSektorPekerjaan2, strAnakSyarikat, strAnakSyarikat1, strAnakSyarikat2, strTarafJawatan, strTarafJawatan1, strTarafJawatan2;
    String seq_count;

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
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }

    }


    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCompletionList(string prefixText, int count)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select mem_new_icno from mem_member where mem_sts_cd='SA' order by mem_new_icno";
                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> countryNames = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    if (sdr.HasRows == true)
                    {
                        while (sdr.Read())
                        {
                            countryNames.Add(sdr["mem_new_icno"].ToString());

                        }
                    }
                    else
                    {
                        countryNames.Add("Rekod Tidak Dijumpai.");
                    }
                }

                con.Close();
                return countryNames;
            }
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
            //hubungan_p1.DataSource = dt;
            //hubungan_p1.DataTextField = "Contact_Name";
            //hubungan_p1.DataValueField = "Contact_Code";
            //hubungan_p1.DataBind();
            //hubungan_p1.Items.Insert(0, new ListItem("--- PILIH ---", ""));
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
        //DataSet Ds = new DataSet();
        //try
        //{
        //    string com = "select Wilayah_Name,Wilayah_Code from Ref_Wilayah order by Wilayah_Name ASC";
        //    SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        //    DataTable dt = new DataTable();
        //    adpt.Fill(dt);
        //    DD_wilayah.DataSource = dt;
        //    DD_wilayah.DataTextField = "Wilayah_Name";
        //    DD_wilayah.DataValueField = "Wilayah_Code";
        //    DD_wilayah.DataBind();
        //    DD_wilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }

    //void Cawangan()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select * from ref_branch WHERE Status = 'A' order by branch_cd ASC";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DD_cawangan.DataSource = dt;
    //        DD_cawangan.DataTextField = "branch_desc";
    //        DD_cawangan.DataValueField = "branch_cd";
    //        DD_cawangan.DataBind();
    //        DD_cawangan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    //protected void ddkaw_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    //-Pusat---------------------------------------------------------------------------------
    //    string cmd6 = "select distinct cawangan_name,cawangan_code from Ref_Cawangan where  wilayah_name='" + DD_wilayah.SelectedItem.Text + "'";
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

    protected void Searchbtn_Click(object sender, EventArgs e)
    {
        DataTable Dt = new DataTable();
        try
        {
            if (TXTNOKP.Text != "")
            {
                DataTable chk_member = new DataTable();
                chk_member = DBCon.Ora_Execute_table("select * from mem_member where mem_new_icno = '" + TXTNOKP.Text + "' and mem_sts_cd = 'SA' and Acc_sts='Y'");
                if (chk_member.Rows.Count != 0)
                {
                    DataTable ddicno = new DataTable();
                    ddicno = DBCon.Ora_Execute_table("select * from jpa_application where app_new_icno='" + TXTNOKP.Text + "'");

                    //Cawangan();
                    wilahBind();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Lakukan Transaksi Kemaskini Pendaftaran Permohonan Pinjaman Bagi Mengemaskini Maklumat Permohonan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    string Select_member_details = "select mm.mem_name,mm.mem_new_icno,mm.mem_member_no,mm.mem_region_cd,mm.mem_branch_cd,mm.mem_centre,mm.mem_address,mm.mem_postcd,mm.mem_negri,case(mm.mem_phone_h) when 'NULL' then '' else mm.mem_phone_h end as mem_phone_h,case(mm.mem_phone_m) when 'NULL' then '' else mm.mem_phone_m end as mem_phone_m,case(mm.mem_phone_o) when 'NULL' then '' else mm.mem_phone_o end as mem_phone_o,mm.mem_bank_acc_no,mm.mem_bank_cd,mm.mem_applicant_type_cd from mem_member as mm where mm.mem_new_icno='" + TXTNOKP.Text + "'";
                    con.Open();
                    var sqlCommand = new SqlCommand(Select_member_details, con);
                    var sqlReader = sqlCommand.ExecuteReader();
                    var ic_count = TXTNOKP.Text.Length;
                    string str_age = string.Empty, c_age = string.Empty;
                    string c_dt1 = DateTime.Now.ToString("yyyy");
                    
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
                    if (ddicno.Rows.Count != 0)
                    {
                        DD_Pelaburan.SelectedValue = ddicno.Rows[0]["app_loan_type_cd"].ToString();
                        dd_tujuan.SelectedValue = ddicno.Rows[0]["app_loan_purpose_cd"].ToString();
                        TextBox2.Text = ddicno.Rows[0]["app_name"].ToString();
                        if (sqlReader.Read() == true)
                        {
                            TextBox13.Text = (string)sqlReader["mem_member_no"];
                            ddkatper.SelectedValue = (string)sqlReader["mem_applicant_type_cd"];
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
                         //   DD_wilayah.SelectedValue = ddicno.Rows[0]["app_region_cd"].ToString();
                        TextArea4.Value = ddicno.Rows[0]["app_mailing_address"].ToString();                        
                        As_pcode.Text = ddicno.Rows[0]["app_mailing_postcode"].ToString();                        
                        DD_NegriBind2.SelectedValue = ddicno.Rows[0]["app_mailing_state_cd"].ToString();


                        TextArea1.Value = ddicno.Rows[0]["app_permnt_address"].ToString();
                        At_pcode.Text = ddicno.Rows[0]["app_permnt_postcode"].ToString();
                            DD_NegriBind1.SelectedValue = ddicno.Rows[0]["app_permnt_state_cd"].ToString();
                            telefon_h.Text = ddicno.Rows[0]["app_phone_h"].ToString();
                            telefon_m.Text = ddicno.Rows[0]["app_phone_m"].ToString();
                            telefon_o.Text = ddicno.Rows[0]["app_phone_o"].ToString();
                            app_bank_acc_no.Text = ddicno.Rows[0]["app_bank_acc_no"].ToString();
                            Bank_details.SelectedValue = ddicno.Rows[0]["app_bank_cd"].ToString();

                    }
                    else
                    {
                        if (sqlReader.Read() == true)
                        {
                           
                            TextBox2.Text = (string)sqlReader["mem_name"];
                            TextBox13.Text = (string)sqlReader["mem_member_no"];
                           // DD_wilayah.SelectedValue = (string)sqlReader["mem_region_cd"];
                            ddkatper.SelectedValue = (string)sqlReader["mem_applicant_type_cd"];
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



                            TextArea1.Value = (string)sqlReader["mem_address"];
                            At_pcode.Text = (string)sqlReader["mem_postcd"];
                            DD_NegriBind1.SelectedValue = (string)sqlReader["mem_negri"];
                            telefon_h.Text = (string)sqlReader["mem_phone_h"];
                            telefon_m.Text = (string)sqlReader["mem_phone_m"];
                            telefon_o.Text = (string)sqlReader["mem_phone_o"];
                            app_bank_acc_no.Text = (string)sqlReader["mem_bank_acc_no"];
                            Bank_details.SelectedValue = (string)sqlReader["mem_bank_cd"];



                            //TextBox17.Text = (string)sqlReader["mem_name"];
                            //TextBox15.Text = (string)sqlReader["mem_new_icno"];
                        }
                    }
                   
                    if (ddicno.Rows.Count != 0)
                    {
                        if (ddicno.Rows[0]["app_sts_cd"].ToString() == "Y")
                        {
                            DataTable ddicno_jpa = new DataTable();
                            ddicno_jpa = DBCon.Ora_Execute_table("select * from jpa_application where app_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "'");
                            if (ddicno_jpa.Rows.Count > 0)
                            {

                                TextBox14.Text = ddicno_jpa.Rows[0]["app_spouse_name"].ToString();
                                TextBox16.Text = ddicno_jpa.Rows[0]["app_spouse_icno"].ToString();
                                TextArea2.Value = ddicno_jpa.Rows[0]["app_spouse_address"].ToString();
                                TextBox18.Text = ddicno_jpa.Rows[0]["app_spouse_phone"].ToString();
                                TextBox56.Text = ddicno_jpa.Rows[0]["app_spouse_postcode"].ToString();
                                DD_NegriBind3.SelectedValue = ddicno_jpa.Rows[0]["app_spouse_state_cd"].ToString(); ;
                            }


                            DataTable ddicno_rel = new DataTable();
                            ddicno_rel = DBCon.Ora_Execute_table("select * from jpa_relative where rel_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' and rel_seq_no='1'");

                            if (ddicno_rel.Rows.Count > 0)
                            {

                                TextBox19.Text = ddicno_rel.Rows[0]["rel_name"].ToString();
                                TextBox21.Text = ddicno_rel.Rows[0]["rel_new_icno"].ToString();
                                TextBox23.Text = ddicno_rel.Rows[0]["rel_phone"].ToString();
                                DD_Hubungan2.SelectedValue = ddicno_rel.Rows[0]["rel_relation_cd"].ToString();

                            }
                            DataTable ddicno_rel2 = new DataTable();
                            ddicno_rel2 = DBCon.Ora_Execute_table("select * from jpa_relative where rel_applcn_no='" + ddicno.Rows[0]["app_applcn_no"].ToString() + "' and rel_seq_no='2'");

                            if (ddicno_rel2.Rows.Count > 0)
                            {

                                TextBox24.Text = ddicno_rel2.Rows[0]["rel_name"].ToString();
                                TextBox25.Text = ddicno_rel2.Rows[0]["rel_new_icno"].ToString();
                                TextBox26.Text = ddicno_rel2.Rows[0]["rel_phone"].ToString();
                                DD_Hubungan3.SelectedValue = ddicno_rel2.Rows[0]["rel_relation_cd"].ToString();

                            }


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
                                if (ddicno_gua.Rows[0]["gua_job_sector_ind"].ToString() != "")
                                {
                                    ddcat.SelectedValue = ddicno_gua.Rows[0]["gua_job_sector_ind"].ToString();
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
                                TextBox31.Text = net_amt.ToString("C").Replace("RM","").Replace("$", "");
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
                            Button3.Visible = true;
                        }
                        else
                        {
                            DD_Pelaburan.SelectedValue = "";
                            dd_tujuan.SelectedValue = "";
                            Button3.Visible = false;
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('No Kp Baru Already Registered. waiting for Approval',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                        }
                    }
                        
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Ahli Tidak Berdaftar, Sila Masukkan Ahli yang Sah',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Ic',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai',{'type': 'warning','title': 'Warning','auto_close': 2000},{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }


    protected void update(object sender, EventArgs e)
    {
        try
        {
            if (TXTNOKP.Text != "")
            {
                if (ddkatper.SelectedValue != "LT")
                {
                    if (TextBox27.Text != "" && TextBox35.Text != "" && TextArea7.Value != "" && TextBox62.Text != "" && DD_Pengenalan2.SelectedItem.Text != "--- PILIH ---" && TextBox28.Text != "" && ddcat.SelectedItem.Text != "--- PILIH ---" && RB11.Checked == true || RB12.Checked == true)
                    {
                        if (TextBox36.Text != "" && TextBox37.Text != "" && TextArea5.Value != "" && TextBox60.Text != "" && DD_Pengenalan3.SelectedItem.Text != "--- PILIH ---" && TextBox38.Text != "" && ddcat2.SelectedItem.Text != "--- PILIH ---" && TJ21.Checked == true || TJ22.Checked == true)
                        {

                            DataTable ddicno1 = new DataTable();
                            ddicno1 = DBCon.Ora_Execute_table("select app_new_icno from jpa_application where app_new_icno='" + TXTNOKP.Text + "'");
                           
                                string userid = Session["New"].ToString();

                                SqlCommand jpa_a = new SqlCommand("insert into jpa_application(app_applcn_no,app_new_icno,app_name,app_loan_type_cd,app_loan_purpose_cd,app_region_cd,app_branch_cd,app_apply_amt,app_apply_dur,app_permnt_address,app_permnt_postcode,app_permnt_state_cd,app_mailing_address,app_mailing_postcode,app_mailing_state_cd,app_phone_h,app_phone_o,app_phone_m,app_bank_acc_no,app_bank_cd,app_spouse_icno,app_spouse_name,app_spouse_phone,app_spouse_address,app_spouse_postcode,app_spouse_state_cd,app_biz_type_ind,app_biz_own_type_ind,app_loan_amt,appl_loan_dur,app_sts_cd,app_crt_id,app_crt_dt,app_upd_id,app_upd_dt,app_applcn_no_count,Created_date,applcn_clsed,app_age) VALUES (@app_applcn_no ,@app_new_icno,@app_name,@app_loan_type_cd,@app_loan_purpose_cd,@app_region_cd,@app_branch_cd,@app_apply_amt,@app_apply_dur,@app_permnt_address, @app_permnt_postcode,@app_permnt_state_cd,@app_mailing_address,@app_mailing_postcode,@app_mailing_state_cd,@app_phone_h,@app_phone_o,@app_phone_m,@app_bank_acc_no,@app_bank_cd,@app_spouse_icno,@app_spouse_name,@app_spouse_phone,@app_spouse_address,@app_spouse_postcode,@app_spouse_state_cd,@app_biz_type_ind,@app_biz_own_type_ind,@app_loan_amt,@appl_loan_dur,@app_sts_cd,@app_crt_id,@app_crt_dt,@app_upd_id,@app_upd_dt,@count_appln,@Created_date,@applcn_clsed,@app_age)", con);

                                SqlCommand jpa_r1 = new SqlCommand("insert into jpa_relative(rel_applcn_no,rel_seq_no,rel_new_icno,rel_name,rel_phone,rel_relation_cd,Created_date) values (@rel_applcn_no ,@rel_seq_no,@rel_new_icno,@rel_name,@rel_phone,@rel_relation_cd,@Created_date)", con);

                                SqlCommand jpa_r2 = new SqlCommand("insert into jpa_relative(rel_applcn_no,rel_seq_no,rel_new_icno,rel_name,rel_phone,rel_relation_cd,Created_date) values (@rel_applcn_no2 ,@rel_seq_no2 ,@rel_new_icno2 ,@rel_name2 ,@rel_phone2 ,@rel_relation_cd2,@Created_date)", con);

                                SqlCommand jpa_k = new SqlCommand("insert into jpa_khairat(tkh_applcn_no,tkh_seq_no,tkh_new_icno,tkh_ic_type_cd,tkh_name,tkh_age,tkh_relation_cd,Created_date) values (@tkh_applcn_no,@tkh_seq_no,@tkh_new_icno,@tkh_ic_type_cd,@tkh_name,@tkh_age,@tkh_relation_cd,@Created_date)", con);

                                SqlCommand jpa_k1 = new SqlCommand("insert into jpa_khairat(tkh_applcn_no,tkh_seq_no,tkh_new_icno,tkh_ic_type_cd,tkh_name,tkh_age,tkh_relation_cd,Created_date) values (@tkh_applcn_no1,@tkh_seq_no1,@tkh_new_icno1,@tkh_ic_type_cd1,@tkh_name1,@tkh_age1,@tkh_relation_cd1,@Created_date1)", con);

                                SqlCommand jpa_g1 = new SqlCommand("insert into jpa_guarantor(gua_applcn_no,gua_seq_no,gua_icno,gua_ic_type_cd,gua_name,gua_phone,gua_permanent_address,gua_permanent_postcode,gua_permanent_state_cd,gua_mailing_address,gua_mailing_postcode,gua_mailing_state_cd,gua_job,gua_job_sector_ind,gua_job_subsidiary_ind,gua_job_status_ind,gua_gross_income,gua_nett_income,gua_employer_name,gua_employer_phone,gua_employer_phone_ext,Created_date) values (@gua_applcn_no,@gua_seq_no,@gua_icno,@gua_ic_type_cd,@gua_name,@gua_phone,@gua_permanent_address,@gua_permanent_postcode,@gua_permanent_state_cd,@gua_mailing_address,@gua_mailing_postcode,@gua_mailing_state_cd,@gua_job,@gua_job_sector_ind,@gua_job_subsidiary_ind,@gua_job_status_ind,@gua_gross_income,@gua_nett_income,@gua_employer_name,@gua_employer_phone,@gua_employer_phone_ext,@Created_date)", con);

                                SqlCommand jpa_g2 = new SqlCommand("insert into jpa_guarantor(gua_applcn_no,gua_seq_no,gua_icno,gua_ic_type_cd,gua_name,gua_phone,gua_permanent_address,gua_permanent_postcode,gua_permanent_state_cd,gua_mailing_address,gua_mailing_postcode,gua_mailing_state_cd,gua_job,gua_job_sector_ind,gua_job_subsidiary_ind,gua_job_status_ind,gua_gross_income,gua_nett_income,gua_employer_name,gua_employer_phone,gua_employer_phone_ext,Created_date) values (@gua_applcn_no2,@gua_seq_no2,@gua_icno2,@gua_ic_type_cd2,@gua_name2,@gua_phone2,@gua_permanent_address2,@gua_permanent_postcode2,@gua_permanent_state_cd2,@gua_mailing_address2,@gua_mailing_postcode2,@gua_mailing_state_cd2,@gua_job2,@gua_job_sector_ind2,@gua_job_subsidiary_ind2,@gua_job_status_ind2,@gua_gross_income2,@gua_nett_income2,@gua_employer_name2,@gua_employer_phone2,@gua_employer_phone_ext2,@Created_date)", con);
                           

                                //SqlCommand jpa_g3 = new SqlCommand("insert into jpa_guarantor(gua_applcn_no,gua_seq_no,gua_icno,gua_ic_type_cd,gua_name,gua_phone,gua_permanent_address,gua_permanent_postcode,gua_permanent_state_cd,gua_mailing_address,gua_mailing_postcode,gua_mailing_state_cd,gua_job,gua_job_sector_ind,gua_job_subsidiary_ind,gua_job_status_ind,gua_gross_income,gua_nett_income,gua_employer_name,gua_employer_phone,gua_employer_phone_ext,Created_date) values (@gua_applcn_no3,@gua_seq_no3,@gua_icno3,@gua_ic_type_cd3,@gua_name3,@gua_phone3,@gua_permanent_address3,@gua_permanent_postcode3,@gua_permanent_state_cd3,@gua_mailing_address3,@gua_mailing_postcode3,@gua_mailing_state_cd3,@gua_job3,@gua_job_sector_ind3,@gua_job_subsidiary_ind3,@gua_job_status_ind3,@gua_gross_income3,@gua_nett_income3,@gua_employer_name3,@gua_employer_phone3,@gua_employer_phone_ext3,@Created_date)", con);

                                SqlCommand max_count = new SqlCommand("SELECT max(app_applcn_no_count) FROM  jpa_application where Created_date IN (SELECT max(Created_date) FROM jpa_application) ", con);
                                con.Open();
                                Object mcount = max_count.ExecuteScalar();
                                if (mcount.ToString() == "")
                                {
                                    //Application No Genration for Default
                                    var count = DateTime.Now.ToString("MMyyyy").PadLeft(11, '0');
                                    string strNoPermohonan = count;
                                    string strNoPermohonan1 = "1";
                                    jpa_a.Parameters.AddWithValue("@app_applcn_no", strNoPermohonan);
                                    jpa_a.Parameters.AddWithValue("@count_appln", strNoPermohonan1);
                                    jpa_r1.Parameters.AddWithValue("@rel_applcn_no", strNoPermohonan);
                                    jpa_r2.Parameters.AddWithValue("@rel_applcn_no2", strNoPermohonan);
                                    jpa_k.Parameters.AddWithValue("@tkh_applcn_no", strNoPermohonan);
                                    jpa_k1.Parameters.AddWithValue("@tkh_applcn_no1", strNoPermohonan);
                                    jpa_g1.Parameters.AddWithValue("@gua_applcn_no", strNoPermohonan);
                                    jpa_g2.Parameters.AddWithValue("@gua_applcn_no2", strNoPermohonan);
                                    //jpa_g3.Parameters.AddWithValue("@gua_applcn_no3", strNoPermohonan);

                                    //Sequence No Genration for Default
                                    jpa_r1.Parameters.AddWithValue("@rel_seq_no", '1');
                                    jpa_r2.Parameters.AddWithValue("@rel_seq_no2", '2');

                                }
                                else
                                {
                                    //Application No Genration for Dynamic
                                    object count1 = (Convert.ToInt32(max_count.ExecuteScalar()) + 1);
                                    string snp = count1 + DateTime.Now.ToString("MMyyyy");
                                    var count = snp.PadLeft(11, '0');
                                    String strNoPermohonan = count;
                                    String strNoPermohonan1 = count1.ToString();
                                    jpa_a.Parameters.AddWithValue("@app_applcn_no", strNoPermohonan);
                                    jpa_a.Parameters.AddWithValue("@count_appln", strNoPermohonan1);
                                    jpa_r1.Parameters.AddWithValue("@rel_applcn_no", strNoPermohonan);
                                    jpa_r2.Parameters.AddWithValue("@rel_applcn_no2", strNoPermohonan);
                                    jpa_k.Parameters.AddWithValue("@tkh_applcn_no", strNoPermohonan);
                                    jpa_k1.Parameters.AddWithValue("@tkh_applcn_no1", strNoPermohonan);
                                    jpa_g1.Parameters.AddWithValue("@gua_applcn_no", strNoPermohonan);
                                    jpa_g2.Parameters.AddWithValue("@gua_applcn_no2", strNoPermohonan);
                                    //jpa_g3.Parameters.AddWithValue("@gua_applcn_no3", strNoPermohonan);

                                    //Sequence No Genration for Default
                                    jpa_r1.Parameters.AddWithValue("@rel_seq_no", '1');
                                    jpa_r2.Parameters.AddWithValue("@rel_seq_no2", '2');
                                }
                                con.Close();
                                //String strNoPermohonan = "";

                                jpa_a.Parameters.AddWithValue("@app_new_icno", TXTNOKP.Text);
                                jpa_a.Parameters.AddWithValue("@app_name", TextBox2.Text);
                                jpa_a.Parameters.AddWithValue("@app_loan_type_cd", DD_Pelaburan.SelectedValue.Trim().ToUpper());
                                jpa_a.Parameters.AddWithValue("@app_loan_purpose_cd", dd_tujuan.SelectedValue.Trim().ToUpper());
                               // jpa_a.Parameters.AddWithValue("@app_region_cd", DD_wilayah.SelectedItem.Value);
                                jpa_a.Parameters.AddWithValue("@app_branch_cd", "");
                                jpa_a.Parameters.AddWithValue("@app_age", app_age.Text);
                                jpa_a.Parameters.AddWithValue("@app_apply_amt", TextBox4.Text);
                                jpa_a.Parameters.AddWithValue("@app_apply_dur", TextBox12.Text);
                                jpa_a.Parameters.AddWithValue("@app_permnt_address", TextArea1.Value.Trim().ToUpper());
                                jpa_a.Parameters.AddWithValue("@app_permnt_postcode", At_pcode.Text);
                                jpa_a.Parameters.AddWithValue("@app_permnt_state_cd", DD_NegriBind1.SelectedItem.Value);
                                jpa_a.Parameters.AddWithValue("@app_mailing_address", TextArea4.Value.Trim().ToUpper());
                                jpa_a.Parameters.AddWithValue("@app_mailing_postcode", As_pcode.Text);
                                jpa_a.Parameters.AddWithValue("@app_mailing_state_cd", DD_NegriBind2.SelectedValue.Trim().ToUpper());
                                jpa_a.Parameters.AddWithValue("@app_phone_h", telefon_h.Text.Trim());
                                jpa_a.Parameters.AddWithValue("@app_phone_o", telefon_o.Text.Trim());
                                jpa_a.Parameters.AddWithValue("@app_phone_m", telefon_m.Text.Trim());
                                jpa_a.Parameters.AddWithValue("@app_bank_acc_no", app_bank_acc_no.Text.Trim());
                                jpa_a.Parameters.AddWithValue("@app_bank_cd", Bank_details.SelectedValue.Trim().ToUpper());
                                jpa_a.Parameters.AddWithValue("@applcn_clsed", "N");
                            // Maklumat Pasangan

                            jpa_a.Parameters.AddWithValue("@app_spouse_icno", TextBox16.Text.Trim());
                                jpa_a.Parameters.AddWithValue("@app_spouse_name", TextBox14.Text.Trim().ToUpper());
                                jpa_a.Parameters.AddWithValue("@app_spouse_phone", TextBox18.Text.Trim().ToUpper());
                                jpa_a.Parameters.AddWithValue("@app_spouse_address", TextArea2.Value.Trim().ToUpper());
                                jpa_a.Parameters.AddWithValue("@app_spouse_postcode", TextBox56.Text);
                                jpa_a.Parameters.AddWithValue("@app_spouse_state_cd", DD_NegriBind3.SelectedValue.Trim());

                                // Maklumat Perniagaan Pemohon


                                status = "P";

                                jpa_a.Parameters.AddWithValue("@app_biz_type_ind", status);

                                JM_status = "R";

                                jpa_a.Parameters.AddWithValue("@app_biz_own_type_ind", JM_status);

                                // Butiran Permohonan
                                jpa_a.Parameters.AddWithValue("@app_loan_amt", TextBox4.Text.Trim());
                                jpa_a.Parameters.AddWithValue("@appl_loan_dur", TextBox12.Text.Trim());
                                jpa_a.Parameters.AddWithValue("@app_sts_cd", "N");
                                jpa_a.Parameters.AddWithValue("@app_crt_id", userid);
                                jpa_a.Parameters.AddWithValue("@app_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                jpa_a.Parameters.AddWithValue("@app_upd_id", "");
                                jpa_a.Parameters.AddWithValue("@app_upd_dt", "");
                                jpa_a.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


                                con.Open();
                                int a = jpa_a.ExecuteNonQuery();
                                con.Close();

                                // Maklumat Waris Terdekat

                                if (TextBox19.Text != "")
                                {

                                    jpa_r1.Parameters.AddWithValue("@rel_new_icno", TextBox21.Text.Trim().ToUpper());
                                    jpa_r1.Parameters.AddWithValue("@rel_name", TextBox19.Text.Trim().ToUpper());
                                    jpa_r1.Parameters.AddWithValue("@rel_phone", TextBox23.Text.Trim());
                                    jpa_r1.Parameters.AddWithValue("@rel_relation_cd", DD_Hubungan2.SelectedValue.Trim().ToUpper());
                                    jpa_r1.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    con.Open();
                                    int r1 = jpa_r1.ExecuteNonQuery();
                                    con.Close();
                                }
                                if (TextBox19.Text != "" && TextBox24.Text != "")
                                {
                                    jpa_r2.Parameters.AddWithValue("@rel_new_icno2", TextBox25.Text.Trim().ToUpper());
                                    jpa_r2.Parameters.AddWithValue("@rel_name2", TextBox24.Text.Trim().ToUpper());
                                    jpa_r2.Parameters.AddWithValue("@rel_phone2", TextBox26.Text.Trim());
                                    jpa_r2.Parameters.AddWithValue("@rel_relation_cd2", DD_Hubungan3.SelectedValue.Trim().ToUpper());
                                    jpa_r2.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                    con.Open();
                                    int r2 = jpa_r2.ExecuteNonQuery();
                                    con.Close();
                                }

                                // Maklumat Tabung Khairat Hutang (Penama 1)



                                jpa_k1.Parameters.AddWithValue("@tkh_new_icno1", TXTNOKP.Text);
                                jpa_k1.Parameters.AddWithValue("@tkh_name1", TextBox2.Text.Trim().ToUpper());
                                jpa_k1.Parameters.AddWithValue("@tkh_seq_no1", "1");
                                jpa_k1.Parameters.AddWithValue("@tkh_ic_type_cd1", "");
                                jpa_k1.Parameters.AddWithValue("@tkh_age1", "");
                                jpa_k1.Parameters.AddWithValue("@tkh_relation_cd1", "");
                                jpa_k1.Parameters.AddWithValue("@Created_date1", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                con.Open();
                                int k1 = jpa_k1.ExecuteNonQuery();
                                con.Close();

                                // Maklumat Tabung Khairat Hutang (Penama 2)


                                //jpa_k.Parameters.AddWithValue("@tkh_new_icno", "");
                                //jpa_k.Parameters.AddWithValue("@tkh_seq_no", "2");
                                //jpa_k.Parameters.AddWithValue("@tkh_ic_type_cd", "");
                                //jpa_k.Parameters.AddWithValue("@tkh_name", "");
                                //jpa_k.Parameters.AddWithValue("@tkh_age","");
                                //jpa_k.Parameters.AddWithValue("@tkh_relation_cd", "");
                                //jpa_k.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                //con.Open();
                                //int k = jpa_k.ExecuteNonQuery();
                                //con.Close();


                                // Insert table on jpa_guarantor
                                // 1

                                jpa_g1.Parameters.AddWithValue("@gua_icno", TextBox28.Text.Trim().ToUpper());
                                jpa_g1.Parameters.AddWithValue("@gua_seq_no", "1");
                                jpa_g1.Parameters.AddWithValue("@gua_ic_type_cd", DD_Pengenalan2.SelectedValue.Trim().ToUpper());
                                jpa_g1.Parameters.AddWithValue("@gua_name", TextBox27.Text.Trim().ToUpper());
                                jpa_g1.Parameters.AddWithValue("@gua_phone", TextBox35.Text.Trim());
                                string gua_add1 = TextArea7.Value.Replace("\r\n", "<br />");
                                jpa_g1.Parameters.AddWithValue("@gua_permanent_address", gua_add1.Trim().ToUpper());
                                jpa_g1.Parameters.AddWithValue("@gua_permanent_postcode", TextBox62.Text.Trim());
                                jpa_g1.Parameters.AddWithValue("@gua_permanent_state_cd", DD_NegriBind5.SelectedItem.Value);
                                string gua_add2 = TextArea8.Value.Replace("\r\n", "<br />");
                                jpa_g1.Parameters.AddWithValue("@gua_mailing_address", gua_add2.Trim().ToUpper());
                                jpa_g1.Parameters.AddWithValue("@gua_mailing_postcode", TextBox63.Text);
                                jpa_g1.Parameters.AddWithValue("@gua_mailing_state_cd", DD_NegriBind6.SelectedValue.Trim().ToUpper());
                                jpa_g1.Parameters.AddWithValue("@gua_job", TextBox29.Text.Trim().ToUpper());
                                if (ddcat.SelectedValue != "")
                                {
                                    strSektorPekerjaan = ddcat.SelectedValue;
                                    jpa_g1.Parameters.AddWithValue("@gua_job_sector_ind", strSektorPekerjaan);
                                }

                                else
                                {
                                    jpa_g1.Parameters.AddWithValue("@gua_job_sector_ind", "");
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

                                jpa_g1.Parameters.AddWithValue("@gua_job_subsidiary_ind", strAnakSyarikat);
                                jpa_g1.Parameters.AddWithValue("@gua_job_status_ind", strTarafJawatan);
                                jpa_g1.Parameters.AddWithValue("@gua_gross_income", TextBox33.Text.Trim().ToUpper());
                                jpa_g1.Parameters.AddWithValue("@gua_nett_income", TextBox31.Text.Trim().ToUpper());
                                jpa_g1.Parameters.AddWithValue("@gua_employer_name", TextBox34.Text.Trim().ToUpper());
                                jpa_g1.Parameters.AddWithValue("@gua_employer_phone", TextBox30.Text.Trim().ToUpper());
                                jpa_g1.Parameters.AddWithValue("@gua_employer_phone_ext", TextBox32.Text);
                                jpa_g1.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                con.Open();
                                int g1 = jpa_g1.ExecuteNonQuery();
                                con.Close();


                                // 2



                                jpa_g2.Parameters.AddWithValue("@gua_icno2", TextBox38.Text.Trim().ToUpper());
                                jpa_g2.Parameters.AddWithValue("@gua_seq_no2", "2");
                                jpa_g2.Parameters.AddWithValue("@gua_ic_type_cd2", DD_Pengenalan3.SelectedValue.Trim().ToUpper());
                                jpa_g2.Parameters.AddWithValue("@gua_name2", TextBox36.Text.Trim().ToUpper());
                                jpa_g2.Parameters.AddWithValue("@gua_phone2", TextBox37.Text.Trim());
                                string gua_add21 = TextArea5.Value.Replace("\r\n", "<br />");
                                jpa_g2.Parameters.AddWithValue("@gua_permanent_address2", gua_add21.Trim().ToUpper());
                                jpa_g2.Parameters.AddWithValue("@gua_permanent_postcode2", TextBox60.Text.Trim());
                                jpa_g2.Parameters.AddWithValue("@gua_permanent_state_cd2", DD_NegriBind7.SelectedItem.Value);
                                string gua_add22 = TextArea6.Value.Replace("\r\n", "<br />");
                                jpa_g2.Parameters.AddWithValue("@gua_mailing_address2", gua_add22.Trim().ToUpper());
                                jpa_g2.Parameters.AddWithValue("@gua_mailing_postcode2", TextBox61.Text);
                                jpa_g2.Parameters.AddWithValue("@gua_mailing_state_cd2", DD_NegriBind8.SelectedValue.Trim().ToUpper());
                                jpa_g2.Parameters.AddWithValue("@gua_job2", TextBox39.Text.Trim().ToUpper());
                                if (ddcat2.SelectedValue != "")
                                {
                                    strSektorPekerjaan1 = ddcat2.SelectedValue;
                                    jpa_g2.Parameters.AddWithValue("@gua_job_sector_ind2", strSektorPekerjaan1);
                                }

                                else
                                {
                                    jpa_g2.Parameters.AddWithValue("@gua_job_sector_ind2", "");
                                }

                                strAnakSyarikat1 = "1";


                                if (TJ21.Checked == true)
                                {
                                    strTarafJawatan1 = "T";
                                }
                                else
                                {
                                    strTarafJawatan1 = "K";
                                }

                                jpa_g2.Parameters.AddWithValue("@gua_job_subsidiary_ind2", strAnakSyarikat1);
                                jpa_g2.Parameters.AddWithValue("@gua_job_status_ind2", strTarafJawatan1);
                                jpa_g2.Parameters.AddWithValue("@gua_gross_income2", TextBox40.Text.Trim().ToUpper());
                                jpa_g2.Parameters.AddWithValue("@gua_nett_income2", TextBox41.Text.Trim().ToUpper());
                                jpa_g2.Parameters.AddWithValue("@gua_employer_name2", TextBox42.Text.Trim().ToUpper());
                                jpa_g2.Parameters.AddWithValue("@gua_employer_phone2", TextBox43.Text.Trim().ToUpper());
                                jpa_g2.Parameters.AddWithValue("@gua_employer_phone_ext2", TextBox44.Text);
                                jpa_g2.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                con.Open();
                                int g2 = jpa_g2.ExecuteNonQuery();
                                con.Close();


                                // 3
                                //if (DD_Pengenalan4.SelectedValue != "" && TextBox47.Text != "")
                                //{
                                //    jpa_g3.Parameters.AddWithValue("@gua_icno3", "");
                                //    jpa_g3.Parameters.AddWithValue("@gua_seq_no3", "3");
                                //    jpa_g3.Parameters.AddWithValue("@gua_ic_type_cd3", "");
                                //    jpa_g3.Parameters.AddWithValue("@gua_name3", TextBox45.Text.Trim().ToUpper());
                                //    jpa_g3.Parameters.AddWithValue("@gua_phone3", TextBox46.Text.Trim());
                                //    string gua_add31 = TextArea9.Value.Replace("\r\n", "<br />");
                                //    jpa_g3.Parameters.AddWithValue("@gua_permanent_address3", gua_add31.Trim().ToUpper());
                                //    jpa_g3.Parameters.AddWithValue("@gua_permanent_postcode3", TextBox58.Text.Trim());
                                //    jpa_g3.Parameters.AddWithValue("@gua_permanent_state_cd3", DD_NegriBind9.SelectedItem.Value);
                                //    string gua_add32 = TextArea10.Value.Replace("\r\n", "<br />");
                                //    jpa_g3.Parameters.AddWithValue("@gua_mailing_address3", gua_add32.Trim().ToUpper());
                                //    jpa_g3.Parameters.AddWithValue("@gua_mailing_postcode3", TextBox59.Text);
                                //    jpa_g3.Parameters.AddWithValue("@gua_mailing_state_cd3", DD_NegriBind10.SelectedValue.Trim().ToUpper());
                                //    jpa_g3.Parameters.AddWithValue("@gua_job3", TextBox48.Text.Trim().ToUpper());
                                //    if (RB31.Checked == true)
                                //    {
                                //        strSektorPekerjaan2 = "GOV";
                                //        jpa_g3.Parameters.AddWithValue("@gua_job_sector_ind3", strSektorPekerjaan2);
                                //    }
                                //    else if (RB32.Checked == true)
                                //    {
                                //        strSektorPekerjaan2 = "GLC";
                                //        jpa_g3.Parameters.AddWithValue("@gua_job_sector_ind3", strSektorPekerjaan2);
                                //    }
                                //    else if (RB33.Checked == true)
                                //    {
                                //        strSektorPekerjaan2 = "GLI";
                                //        jpa_g3.Parameters.AddWithValue("@gua_job_sector_ind3", strSektorPekerjaan2);
                                //    }
                                //    else if (RB34.Checked == true)
                                //    {
                                //        jpa_g3.Parameters.AddWithValue("@gua_job_sector_ind3", "");
                                //    }

                                //    if (CheckBox2.Checked == true)
                                //    {
                                //        strAnakSyarikat2 = "1";
                                //    }
                                //    else
                                //    {
                                //        strAnakSyarikat2 = "0";
                                //    }

                                //    if (TJ31.Checked == true)
                                //    {
                                //        strTarafJawatan2 = "T";
                                //    }
                                //    else
                                //    {
                                //        strTarafJawatan2 = "K";
                                //    }

                                //    jpa_g3.Parameters.AddWithValue("@gua_job_subsidiary_ind3", strAnakSyarikat2);
                                //    jpa_g3.Parameters.AddWithValue("@gua_job_status_ind3", strTarafJawatan2);
                                //    jpa_g3.Parameters.AddWithValue("@gua_gross_income3", TextBox49.Text.Trim().ToUpper());
                                //    jpa_g3.Parameters.AddWithValue("@gua_nett_income3", TextBox50.Text.Trim().ToUpper());
                                //    jpa_g3.Parameters.AddWithValue("@gua_employer_name3", TextBox51.Text.Trim().ToUpper());
                                //    jpa_g3.Parameters.AddWithValue("@gua_employer_phone3", TextBox52.Text.Trim().ToUpper());
                                //    jpa_g3.Parameters.AddWithValue("@gua_employer_phone_ext3", TextBox53.Text);
                                //    jpa_g3.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                //    con.Open();
                                //    int g3 = jpa_g3.ExecuteNonQuery();
                                //    con.Close();
                                //}

                                Button6.Visible = true;
                                Button3.Visible = false;
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                            

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Di Mangan Mandatory.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                        }
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Di Mangan Mandatory.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {



                    DataTable ddicno1 = new DataTable();
                    ddicno1 = DBCon.Ora_Execute_table("select app_new_icno from jpa_application where app_new_icno='" + TXTNOKP.Text + "'");
                    if (ddicno1.Rows.Count == 0)
                    {
                        string userid = Session["New"].ToString();

                        SqlCommand jpa_a = new SqlCommand("insert into jpa_application(app_applcn_no,app_new_icno,app_name,app_loan_type_cd,app_loan_purpose_cd,app_region_cd,app_branch_cd,app_apply_amt,app_apply_dur,app_permnt_address,app_permnt_postcode,app_permnt_state_cd,app_mailing_address,app_mailing_postcode,app_mailing_state_cd,app_phone_h,app_phone_o,app_phone_m,app_bank_acc_no,app_bank_cd,app_spouse_icno,app_spouse_name,app_spouse_phone,app_spouse_address,app_spouse_postcode,app_spouse_state_cd,app_biz_type_ind,app_biz_own_type_ind,app_loan_amt,appl_loan_dur,app_sts_cd,app_crt_id,app_crt_dt,app_upd_id,app_upd_dt,app_applcn_no_count,Created_date,applcn_clsed,app_age) VALUES (@app_applcn_no ,@app_new_icno,@app_name,@app_loan_type_cd,@app_loan_purpose_cd,@app_region_cd,@app_branch_cd,@app_apply_amt,@app_apply_dur,@app_permnt_address, @app_permnt_postcode,@app_permnt_state_cd,@app_mailing_address,@app_mailing_postcode,@app_mailing_state_cd,@app_phone_h,@app_phone_o,@app_phone_m,@app_bank_acc_no,@app_bank_cd,@app_spouse_icno,@app_spouse_name,@app_spouse_phone,@app_spouse_address,@app_spouse_postcode,@app_spouse_state_cd,@app_biz_type_ind,@app_biz_own_type_ind,@app_loan_amt,@appl_loan_dur,@app_sts_cd,@app_crt_id,@app_crt_dt,@app_upd_id,@app_upd_dt,@count_appln,@Created_date,@applcn_clsed,@app_age)", con);

                        SqlCommand jpa_r1 = new SqlCommand("insert into jpa_relative(rel_applcn_no,rel_seq_no,rel_new_icno,rel_name,rel_phone,rel_relation_cd,Created_date) values (@rel_applcn_no ,@rel_seq_no,@rel_new_icno,@rel_name,@rel_phone,@rel_relation_cd,@Created_date)", con);

                        SqlCommand jpa_r2 = new SqlCommand("insert into jpa_relative(rel_applcn_no,rel_seq_no,rel_new_icno,rel_name,rel_phone,rel_relation_cd,Created_date) values (@rel_applcn_no2 ,@rel_seq_no2 ,@rel_new_icno2 ,@rel_name2 ,@rel_phone2 ,@rel_relation_cd2,@Created_date)", con);

                        SqlCommand jpa_k = new SqlCommand("insert into jpa_khairat(tkh_applcn_no,tkh_seq_no,tkh_new_icno,tkh_ic_type_cd,tkh_name,tkh_age,tkh_relation_cd,Created_date) values (@tkh_applcn_no,@tkh_seq_no,@tkh_new_icno,@tkh_ic_type_cd,@tkh_name,@tkh_age,@tkh_relation_cd,@Created_date)", con);

                        SqlCommand jpa_k1 = new SqlCommand("insert into jpa_khairat(tkh_applcn_no,tkh_seq_no,tkh_new_icno,tkh_ic_type_cd,tkh_name,tkh_age,tkh_relation_cd,Created_date) values (@tkh_applcn_no1,@tkh_seq_no1,@tkh_new_icno1,@tkh_ic_type_cd1,@tkh_name1,@tkh_age1,@tkh_relation_cd1,@Created_date1)", con);

                        //SqlCommand jpa_g1 = new SqlCommand("insert into jpa_guarantor(gua_applcn_no,gua_seq_no,gua_icno,gua_ic_type_cd,gua_name,gua_phone,gua_permanent_address,gua_permanent_postcode,gua_permanent_state_cd,gua_mailing_address,gua_mailing_postcode,gua_mailing_state_cd,gua_job,gua_job_sector_ind,gua_job_subsidiary_ind,gua_job_status_ind,gua_gross_income,gua_nett_income,gua_employer_name,gua_employer_phone,gua_employer_phone_ext,Created_date) values (@gua_applcn_no,@gua_seq_no,@gua_icno,@gua_ic_type_cd,@gua_name,@gua_phone,@gua_permanent_address,@gua_permanent_postcode,@gua_permanent_state_cd,@gua_mailing_address,@gua_mailing_postcode,@gua_mailing_state_cd,@gua_job,@gua_job_sector_ind,@gua_job_subsidiary_ind,@gua_job_status_ind,@gua_gross_income,@gua_nett_income,@gua_employer_name,@gua_employer_phone,@gua_employer_phone_ext,@Created_date)", con);

                        //SqlCommand jpa_g2 = new SqlCommand("insert into jpa_guarantor(gua_applcn_no,gua_seq_no,gua_icno,gua_ic_type_cd,gua_name,gua_phone,gua_permanent_address,gua_permanent_postcode,gua_permanent_state_cd,gua_mailing_address,gua_mailing_postcode,gua_mailing_state_cd,gua_job,gua_job_sector_ind,gua_job_subsidiary_ind,gua_job_status_ind,gua_gross_income,gua_nett_income,gua_employer_name,gua_employer_phone,gua_employer_phone_ext,Created_date) values (@gua_applcn_no2,@gua_seq_no2,@gua_icno2,@gua_ic_type_cd2,@gua_name2,@gua_phone2,@gua_permanent_address2,@gua_permanent_postcode2,@gua_permanent_state_cd2,@gua_mailing_address2,@gua_mailing_postcode2,@gua_mailing_state_cd2,@gua_job2,@gua_job_sector_ind2,@gua_job_subsidiary_ind2,@gua_job_status_ind2,@gua_gross_income2,@gua_nett_income2,@gua_employer_name2,@gua_employer_phone2,@gua_employer_phone_ext2,@Created_date)", con);

                        //SqlCommand jpa_g3 = new SqlCommand("insert into jpa_guarantor(gua_applcn_no,gua_seq_no,gua_icno,gua_ic_type_cd,gua_name,gua_phone,gua_permanent_address,gua_permanent_postcode,gua_permanent_state_cd,gua_mailing_address,gua_mailing_postcode,gua_mailing_state_cd,gua_job,gua_job_sector_ind,gua_job_subsidiary_ind,gua_job_status_ind,gua_gross_income,gua_nett_income,gua_employer_name,gua_employer_phone,gua_employer_phone_ext,Created_date) values (@gua_applcn_no3,@gua_seq_no3,@gua_icno3,@gua_ic_type_cd3,@gua_name3,@gua_phone3,@gua_permanent_address3,@gua_permanent_postcode3,@gua_permanent_state_cd3,@gua_mailing_address3,@gua_mailing_postcode3,@gua_mailing_state_cd3,@gua_job3,@gua_job_sector_ind3,@gua_job_subsidiary_ind3,@gua_job_status_ind3,@gua_gross_income3,@gua_nett_income3,@gua_employer_name3,@gua_employer_phone3,@gua_employer_phone_ext3,@Created_date)", con);

                        SqlCommand max_count = new SqlCommand("SELECT max(app_applcn_no_count) FROM  jpa_application where Created_date IN (SELECT max(Created_date) FROM jpa_application) ", con);
                        con.Open();
                        Object mcount = max_count.ExecuteScalar();
                        if (mcount.ToString() == "")
                        {
                            object count1 = 1;
                            string snp = count1 + DateTime.Now.ToString("MMyyyy");
                            //Application No Genration for Default
                            var count = snp.PadLeft(11, '0');
                            string strNoPermohonan = count;
                            string strNoPermohonan1 = count1.ToString();
                            jpa_a.Parameters.AddWithValue("@app_applcn_no", strNoPermohonan);
                            jpa_a.Parameters.AddWithValue("@count_appln", strNoPermohonan1);
                            jpa_r1.Parameters.AddWithValue("@rel_applcn_no", strNoPermohonan);
                            jpa_r2.Parameters.AddWithValue("@rel_applcn_no2", strNoPermohonan);
                            jpa_k.Parameters.AddWithValue("@tkh_applcn_no", strNoPermohonan);
                            jpa_k1.Parameters.AddWithValue("@tkh_applcn_no1", strNoPermohonan);
                            //jpa_g1.Parameters.AddWithValue("@gua_applcn_no", strNoPermohonan);
                            //jpa_g2.Parameters.AddWithValue("@gua_applcn_no2", strNoPermohonan);
                            //jpa_g3.Parameters.AddWithValue("@gua_applcn_no3", strNoPermohonan);

                            //Sequence No Genration for Default
                            jpa_r1.Parameters.AddWithValue("@rel_seq_no", '1');
                            jpa_r2.Parameters.AddWithValue("@rel_seq_no2", '2');

                        }
                        else
                        {
                            //Application No Genration for Dynamic
                            object count1 = (Convert.ToInt32(max_count.ExecuteScalar()) + 1);
                            string snp = count1 + DateTime.Now.ToString("MMyyyy");
                            var count = snp.PadLeft(11, '0');
                            String strNoPermohonan = count;
                            String strNoPermohonan1 = count1.ToString();
                            jpa_a.Parameters.AddWithValue("@app_applcn_no", strNoPermohonan);
                            jpa_a.Parameters.AddWithValue("@count_appln", strNoPermohonan1);
                            jpa_r1.Parameters.AddWithValue("@rel_applcn_no", strNoPermohonan);
                            jpa_r2.Parameters.AddWithValue("@rel_applcn_no2", strNoPermohonan);
                            jpa_k.Parameters.AddWithValue("@tkh_applcn_no", strNoPermohonan);
                            jpa_k1.Parameters.AddWithValue("@tkh_applcn_no1", strNoPermohonan);
                            //jpa_g1.Parameters.AddWithValue("@gua_applcn_no", strNoPermohonan);
                            //jpa_g2.Parameters.AddWithValue("@gua_applcn_no2", strNoPermohonan);
                            //jpa_g3.Parameters.AddWithValue("@gua_applcn_no3", strNoPermohonan);

                            //Sequence No Genration for Default
                            jpa_r1.Parameters.AddWithValue("@rel_seq_no", '1');
                            jpa_r2.Parameters.AddWithValue("@rel_seq_no2", '2');
                        }
                        con.Close();
                        //String strNoPermohonan = "";

                        jpa_a.Parameters.AddWithValue("@app_new_icno", TXTNOKP.Text);
                        jpa_a.Parameters.AddWithValue("@app_name", TextBox2.Text);
                        jpa_a.Parameters.AddWithValue("@app_loan_type_cd", DD_Pelaburan.SelectedValue.Trim().ToUpper());
                        jpa_a.Parameters.AddWithValue("@app_loan_purpose_cd", dd_tujuan.SelectedValue.Trim().ToUpper());
                      //  jpa_a.Parameters.AddWithValue("@app_region_cd", DD_wilayah.SelectedItem.Value);
                        jpa_a.Parameters.AddWithValue("@app_branch_cd", "");
                        jpa_a.Parameters.AddWithValue("@app_age", app_age.Text);
                        jpa_a.Parameters.AddWithValue("@app_apply_amt", TextBox4.Text);
                        jpa_a.Parameters.AddWithValue("@app_apply_dur", TextBox12.Text);
                        jpa_a.Parameters.AddWithValue("@app_permnt_address", TextArea1.Value.Trim().ToUpper());
                        jpa_a.Parameters.AddWithValue("@app_permnt_postcode", At_pcode.Text);
                        jpa_a.Parameters.AddWithValue("@app_permnt_state_cd", DD_NegriBind1.SelectedItem.Value);
                        jpa_a.Parameters.AddWithValue("@app_mailing_address", TextArea4.Value.Trim().ToUpper());
                        jpa_a.Parameters.AddWithValue("@app_mailing_postcode", As_pcode.Text);
                        jpa_a.Parameters.AddWithValue("@app_mailing_state_cd", DD_NegriBind2.SelectedValue.Trim().ToUpper());
                        jpa_a.Parameters.AddWithValue("@app_phone_h", telefon_h.Text.Trim());
                        jpa_a.Parameters.AddWithValue("@app_phone_o", telefon_o.Text.Trim());
                        jpa_a.Parameters.AddWithValue("@app_phone_m", telefon_m.Text.Trim());
                        jpa_a.Parameters.AddWithValue("@app_bank_acc_no", app_bank_acc_no.Text.Trim());
                        jpa_a.Parameters.AddWithValue("@app_bank_cd", Bank_details.SelectedValue.Trim().ToUpper());
                        jpa_a.Parameters.AddWithValue("@applcn_clsed", "N");

                        // Maklumat Pasangan

                        jpa_a.Parameters.AddWithValue("@app_spouse_icno", TextBox16.Text.Trim());
                        jpa_a.Parameters.AddWithValue("@app_spouse_name", TextBox14.Text.Trim().ToUpper());
                        jpa_a.Parameters.AddWithValue("@app_spouse_phone", TextBox18.Text.Trim().ToUpper());
                        jpa_a.Parameters.AddWithValue("@app_spouse_address", TextArea2.Value.Trim().ToUpper());
                        jpa_a.Parameters.AddWithValue("@app_spouse_postcode", TextBox56.Text);
                        jpa_a.Parameters.AddWithValue("@app_spouse_state_cd", DD_NegriBind3.SelectedValue.Trim());

                        // Maklumat Perniagaan Pemohon


                        status = "P";

                        jpa_a.Parameters.AddWithValue("@app_biz_type_ind", status);

                        JM_status = "R";

                        jpa_a.Parameters.AddWithValue("@app_biz_own_type_ind", JM_status);

                        // Butiran Permohonan
                        jpa_a.Parameters.AddWithValue("@app_loan_amt", TextBox4.Text.Trim());
                        jpa_a.Parameters.AddWithValue("@appl_loan_dur", TextBox12.Text.Trim());
                        jpa_a.Parameters.AddWithValue("@app_sts_cd", "N");
                        jpa_a.Parameters.AddWithValue("@app_crt_id", userid);
                        jpa_a.Parameters.AddWithValue("@app_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        jpa_a.Parameters.AddWithValue("@app_upd_id", "");
                        jpa_a.Parameters.AddWithValue("@app_upd_dt", "");
                        jpa_a.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


                        con.Open();
                        int a = jpa_a.ExecuteNonQuery();
                        con.Close();

                        // Maklumat Waris Terdekat

                        if (TextBox19.Text != "")
                        {

                            jpa_r1.Parameters.AddWithValue("@rel_new_icno", TextBox21.Text.Trim().ToUpper());
                            jpa_r1.Parameters.AddWithValue("@rel_name", TextBox19.Text.Trim().ToUpper());
                            jpa_r1.Parameters.AddWithValue("@rel_phone", TextBox23.Text.Trim());
                            jpa_r1.Parameters.AddWithValue("@rel_relation_cd", DD_Hubungan2.SelectedValue.Trim().ToUpper());
                            jpa_r1.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            con.Open();
                            int r1 = jpa_r1.ExecuteNonQuery();
                            con.Close();
                        }
                        if (TextBox19.Text != "" && TextBox24.Text != "")
                        {
                            jpa_r2.Parameters.AddWithValue("@rel_new_icno2", TextBox25.Text.Trim().ToUpper());
                            jpa_r2.Parameters.AddWithValue("@rel_name2", TextBox24.Text.Trim().ToUpper());
                            jpa_r2.Parameters.AddWithValue("@rel_phone2", TextBox26.Text.Trim());
                            jpa_r2.Parameters.AddWithValue("@rel_relation_cd2", DD_Hubungan3.SelectedValue.Trim().ToUpper());
                            jpa_r2.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            con.Open();
                            int r2 = jpa_r2.ExecuteNonQuery();
                            con.Close();
                        }

                        // Maklumat Tabung Khairat Hutang (Penama 1)



                        jpa_k1.Parameters.AddWithValue("@tkh_new_icno1", TXTNOKP.Text);
                        jpa_k1.Parameters.AddWithValue("@tkh_name1", TextBox2.Text.Trim().ToUpper());
                        jpa_k1.Parameters.AddWithValue("@tkh_seq_no1", "1");
                        jpa_k1.Parameters.AddWithValue("@tkh_ic_type_cd1", "");
                        jpa_k1.Parameters.AddWithValue("@tkh_age1", "");
                        jpa_k1.Parameters.AddWithValue("@tkh_relation_cd1", "");
                        jpa_k1.Parameters.AddWithValue("@Created_date1", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        con.Open();
                        int k1 = jpa_k1.ExecuteNonQuery();
                        con.Close();

                        // Maklumat Tabung Khairat Hutang (Penama 2)


                        //jpa_k.Parameters.AddWithValue("@tkh_new_icno", "");
                        //jpa_k.Parameters.AddWithValue("@tkh_seq_no", "2");
                        //jpa_k.Parameters.AddWithValue("@tkh_ic_type_cd", "");
                        //jpa_k.Parameters.AddWithValue("@tkh_name", "");
                        //jpa_k.Parameters.AddWithValue("@tkh_age","");
                        //jpa_k.Parameters.AddWithValue("@tkh_relation_cd", "");
                        //jpa_k.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        //con.Open();
                        //int k = jpa_k.ExecuteNonQuery();
                        //con.Close();


                        // Insert table on jpa_guarantor
                        // 1
                        //if (DD_Pengenalan2.SelectedItem.Text != "--- PILIH ---" && TextBox28.Text != "")
                        //{
                        //    jpa_g1.Parameters.AddWithValue("@gua_icno", TextBox28.Text.Trim().ToUpper());
                        //    jpa_g1.Parameters.AddWithValue("@gua_seq_no", "1");
                        //    jpa_g1.Parameters.AddWithValue("@gua_ic_type_cd", DD_Pengenalan2.SelectedValue.Trim().ToUpper());
                        //    jpa_g1.Parameters.AddWithValue("@gua_name", TextBox27.Text.Trim().ToUpper());
                        //    jpa_g1.Parameters.AddWithValue("@gua_phone", TextBox35.Text.Trim());
                        //    string gua_add1 = TextArea7.Value.Replace("\r\n", "<br />");
                        //    jpa_g1.Parameters.AddWithValue("@gua_permanent_address", gua_add1.Trim().ToUpper());
                        //    jpa_g1.Parameters.AddWithValue("@gua_permanent_postcode", TextBox62.Text.Trim());
                        //    jpa_g1.Parameters.AddWithValue("@gua_permanent_state_cd", DD_NegriBind5.SelectedItem.Value);
                        //    string gua_add2 = TextArea8.Value.Replace("\r\n", "<br />");
                        //    jpa_g1.Parameters.AddWithValue("@gua_mailing_address", gua_add2.Trim().ToUpper());
                        //    jpa_g1.Parameters.AddWithValue("@gua_mailing_postcode", TextBox63.Text);
                        //    jpa_g1.Parameters.AddWithValue("@gua_mailing_state_cd", DD_NegriBind6.SelectedValue.Trim().ToUpper());
                        //    jpa_g1.Parameters.AddWithValue("@gua_job", TextBox29.Text.Trim().ToUpper());
                        //    if (ddcat.SelectedValue != "")
                        //    {
                        //        strSektorPekerjaan = ddcat.SelectedValue;
                        //        jpa_g1.Parameters.AddWithValue("@gua_job_sector_ind", strSektorPekerjaan);
                        //    }

                        //    else
                        //    {
                        //        jpa_g1.Parameters.AddWithValue("@gua_job_sector_ind", "");
                        //    }


                        //    strAnakSyarikat = "1";


                        //    if (RB11.Checked == true)
                        //    {
                        //        strTarafJawatan = "T";
                        //    }
                        //    else
                        //    {
                        //        strTarafJawatan = "K";
                        //    }

                        //    jpa_g1.Parameters.AddWithValue("@gua_job_subsidiary_ind", strAnakSyarikat);
                        //    jpa_g1.Parameters.AddWithValue("@gua_job_status_ind", strTarafJawatan);
                        //    jpa_g1.Parameters.AddWithValue("@gua_gross_income", TextBox31.Text.Trim().ToUpper());
                        //    jpa_g1.Parameters.AddWithValue("@gua_nett_income", TextBox33.Text.Trim().ToUpper());
                        //    jpa_g1.Parameters.AddWithValue("@gua_employer_name", TextBox34.Text.Trim().ToUpper());
                        //    jpa_g1.Parameters.AddWithValue("@gua_employer_phone", TextBox30.Text.Trim().ToUpper());
                        //    jpa_g1.Parameters.AddWithValue("@gua_employer_phone_ext", TextBox32.Text);
                        //    jpa_g1.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        //    con.Open();
                        //    int g1 = jpa_g1.ExecuteNonQuery();
                        //    con.Close();
                        //}

                        // 2

                        //if (DD_Pengenalan3.SelectedValue != "" && TextBox38.Text != "")
                        //{
                        //    jpa_g2.Parameters.AddWithValue("@gua_icno2", TextBox38.Text.Trim().ToUpper());
                        //    jpa_g2.Parameters.AddWithValue("@gua_seq_no2", "2");
                        //    jpa_g2.Parameters.AddWithValue("@gua_ic_type_cd2", DD_Pengenalan3.SelectedValue.Trim().ToUpper());
                        //    jpa_g2.Parameters.AddWithValue("@gua_name2", TextBox36.Text.Trim().ToUpper());
                        //    jpa_g2.Parameters.AddWithValue("@gua_phone2", TextBox37.Text.Trim());
                        //    string gua_add21 = TextArea5.Value.Replace("\r\n", "<br />");
                        //    jpa_g2.Parameters.AddWithValue("@gua_permanent_address2", gua_add21.Trim().ToUpper());
                        //    jpa_g2.Parameters.AddWithValue("@gua_permanent_postcode2", TextBox60.Text.Trim());
                        //    jpa_g2.Parameters.AddWithValue("@gua_permanent_state_cd2", DD_NegriBind7.SelectedItem.Value);
                        //    string gua_add22 = TextArea6.Value.Replace("\r\n", "<br />");
                        //    jpa_g2.Parameters.AddWithValue("@gua_mailing_address2", gua_add22.Trim().ToUpper());
                        //    jpa_g2.Parameters.AddWithValue("@gua_mailing_postcode2", TextBox61.Text);
                        //    jpa_g2.Parameters.AddWithValue("@gua_mailing_state_cd2", DD_NegriBind8.SelectedValue.Trim().ToUpper());
                        //    jpa_g2.Parameters.AddWithValue("@gua_job2", TextBox39.Text.Trim().ToUpper());
                        //    if (ddcat2.SelectedValue != "")
                        //    {
                        //        strSektorPekerjaan1 = ddcat2.SelectedValue;
                        //        jpa_g2.Parameters.AddWithValue("@gua_job_sector_ind2", strSektorPekerjaan1);
                        //    }

                        //    else
                        //    {
                        //        jpa_g2.Parameters.AddWithValue("@gua_job_sector_ind2", "");
                        //    }




                        //    if (TJ21.Checked == true)
                        //    {
                        //        strTarafJawatan1 = "T";
                        //    }
                        //    else
                        //    {
                        //        strTarafJawatan1 = "K";
                        //    }

                        //    jpa_g2.Parameters.AddWithValue("@gua_job_subsidiary_ind2", strAnakSyarikat1);
                        //    jpa_g2.Parameters.AddWithValue("@gua_job_status_ind2", strTarafJawatan1);
                        //    jpa_g2.Parameters.AddWithValue("@gua_gross_income2", TextBox40.Text.Trim().ToUpper());
                        //    jpa_g2.Parameters.AddWithValue("@gua_nett_income2", TextBox41.Text.Trim().ToUpper());
                        //    jpa_g2.Parameters.AddWithValue("@gua_employer_name2", TextBox42.Text.Trim().ToUpper());
                        //    jpa_g2.Parameters.AddWithValue("@gua_employer_phone2", TextBox43.Text.Trim().ToUpper());
                        //    jpa_g2.Parameters.AddWithValue("@gua_employer_phone_ext2", TextBox44.Text);
                        //    jpa_g2.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        //    con.Open();
                        //    int g2 = jpa_g2.ExecuteNonQuery();
                        //    con.Close();
                        //}

                        // 3
                        //if (DD_Pengenalan4.SelectedValue != "" && TextBox47.Text != "")
                        //{
                        //    jpa_g3.Parameters.AddWithValue("@gua_icno3", "");
                        //    jpa_g3.Parameters.AddWithValue("@gua_seq_no3", "3");
                        //    jpa_g3.Parameters.AddWithValue("@gua_ic_type_cd3", "");
                        //    jpa_g3.Parameters.AddWithValue("@gua_name3", TextBox45.Text.Trim().ToUpper());
                        //    jpa_g3.Parameters.AddWithValue("@gua_phone3", TextBox46.Text.Trim());
                        //    string gua_add31 = TextArea9.Value.Replace("\r\n", "<br />");
                        //    jpa_g3.Parameters.AddWithValue("@gua_permanent_address3", gua_add31.Trim().ToUpper());
                        //    jpa_g3.Parameters.AddWithValue("@gua_permanent_postcode3", TextBox58.Text.Trim());
                        //    jpa_g3.Parameters.AddWithValue("@gua_permanent_state_cd3", DD_NegriBind9.SelectedItem.Value);
                        //    string gua_add32 = TextArea10.Value.Replace("\r\n", "<br />");
                        //    jpa_g3.Parameters.AddWithValue("@gua_mailing_address3", gua_add32.Trim().ToUpper());
                        //    jpa_g3.Parameters.AddWithValue("@gua_mailing_postcode3", TextBox59.Text);
                        //    jpa_g3.Parameters.AddWithValue("@gua_mailing_state_cd3", DD_NegriBind10.SelectedValue.Trim().ToUpper());
                        //    jpa_g3.Parameters.AddWithValue("@gua_job3", TextBox48.Text.Trim().ToUpper());
                        //    if (RB31.Checked == true)
                        //    {
                        //        strSektorPekerjaan2 = "GOV";
                        //        jpa_g3.Parameters.AddWithValue("@gua_job_sector_ind3", strSektorPekerjaan2);
                        //    }
                        //    else if (RB32.Checked == true)
                        //    {
                        //        strSektorPekerjaan2 = "GLC";
                        //        jpa_g3.Parameters.AddWithValue("@gua_job_sector_ind3", strSektorPekerjaan2);
                        //    }
                        //    else if (RB33.Checked == true)
                        //    {
                        //        strSektorPekerjaan2 = "GLI";
                        //        jpa_g3.Parameters.AddWithValue("@gua_job_sector_ind3", strSektorPekerjaan2);
                        //    }
                        //    else if (RB34.Checked == true)
                        //    {
                        //        jpa_g3.Parameters.AddWithValue("@gua_job_sector_ind3", "");
                        //    }

                        //    if (CheckBox2.Checked == true)
                        //    {
                        //        strAnakSyarikat2 = "1";
                        //    }
                        //    else
                        //    {
                        //        strAnakSyarikat2 = "0";
                        //    }

                        //    if (TJ31.Checked == true)
                        //    {
                        //        strTarafJawatan2 = "T";
                        //    }
                        //    else
                        //    {
                        //        strTarafJawatan2 = "K";
                        //    }

                        //    jpa_g3.Parameters.AddWithValue("@gua_job_subsidiary_ind3", strAnakSyarikat2);
                        //    jpa_g3.Parameters.AddWithValue("@gua_job_status_ind3", strTarafJawatan2);
                        //    jpa_g3.Parameters.AddWithValue("@gua_gross_income3", TextBox49.Text.Trim().ToUpper());
                        //    jpa_g3.Parameters.AddWithValue("@gua_nett_income3", TextBox50.Text.Trim().ToUpper());
                        //    jpa_g3.Parameters.AddWithValue("@gua_employer_name3", TextBox51.Text.Trim().ToUpper());
                        //    jpa_g3.Parameters.AddWithValue("@gua_employer_phone3", TextBox52.Text.Trim().ToUpper());
                        //    jpa_g3.Parameters.AddWithValue("@gua_employer_phone_ext3", TextBox53.Text);
                        //    jpa_g3.Parameters.AddWithValue("@Created_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        //    con.Open();
                        //    int g3 = jpa_g3.ExecuteNonQuery();
                        //    con.Close();
                        //}

                        Button6.Visible = true;
                        Button3.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Ic No wujud',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
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
                    ddicno = DBCon.Ora_Execute_table("select top(1) app_new_icno,app_applcn_no from jpa_application where app_new_icno='" + TXTNOKP.Text + "' order by Created_date desc");

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

                    // v8 = TextBox7.Text;
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
                    //v22 = TextBox17.Text;
                    //v23 = app_age.Text;
                    //v24 = TextBox15.Text;

                    //3-2
                    //v25 = TextBox9.Text;
                    //v26 = TextBox10.Text;
                    //v27 = TextBox11.Text;
                    //if (DD_Hubungan1.SelectedValue != "")
                    //{
                    //    v28 = DD_Hubungan1.SelectedItem.Text;
                    //}
                    //else
                    //{
                    //    v28 = "";
                    //}
                    //if (DD_Pengenalan1.SelectedValue != "")
                    //{
                    //    v29 = DD_Pengenalan1.SelectedItem.Text;
                    //}
                    //else
                    //{
                    //    v29 = "";
                    //}

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
                    v46 = "";
                    v47 = "";         
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
                    if (ddcat.SelectedItem.Text!= "")
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
                    if (ddcat2.SelectedItem.Text != "")
                    {
                        mv142 = ddcat2.SelectedItem.Text;
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
                    //if (RB31.Checked == true)
                    //{
                    //    mv242 = "Agensi / Jabatan / Kementerian Kerajaan Malaysia";
                    //}
                    //else if (RB32.Checked == true)
                    //{
                    //    mv242 = "Syarikat Berkaitan Kerajaan (GLC)";
                    //}
                    //else if (RB33.Checked == true)
                    //{
                    //    mv242 = "Syarikat Pelaburan Berkaitan Kerajaan (GLIC)";
                    //}
                    //else if (RB34.Checked == true)
                    //{
                    //    mv242 = "Syarikat Berstatus Berhad";
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
                    //if (TextBox49.Text != "")
                    //{
                    //    mv244 = double.Parse(TextBox49.Text).ToString("C").Replace("$", "").Replace("RM", "");
                    //}
                    //if (TextBox50.Text != "")
                    //{
                    //    mv245 = double.Parse(TextBox50.Text).ToString("C").Replace("$", "").Replace("RM", "");
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


                    Response.AddHeader("content-disposition", "attachment; filename=PENDAFTARAN_PERMOHONAN_PEMBIAYAAN_" + TXTNOKP.Text + "." + extension);

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

    protected void Reset_btn_btm(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }


}