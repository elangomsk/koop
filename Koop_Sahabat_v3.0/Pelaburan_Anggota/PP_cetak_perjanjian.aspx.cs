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

public partial class PP_cetak_perjanjian : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    string level, userid;
    string bcode;
    string wcode;
    string status;
    DataTable wilayah = new DataTable();
    DataTable caw = new DataTable();
    DataTable pusat = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Button3);
        scriptManager.RegisterPostBackControl(this.Button4);
        //scriptManager.RegisterPostBackControl(this.Button8);
        //scriptManager.RegisterPostBackControl(this.Button5);
        //scriptManager.RegisterPostBackControl(this.Button6);
        //scriptManager.RegisterPostBackControl(this.Button7);
        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();

                TextBox2.Attributes.Add("Readonly", "Readonly");
                //TextBox3.Attributes.Add("Readonly", "Readonly");
                //TextBox4.Attributes.Add("Readonly", "Readonly");
                TextBox8.Attributes.Add("Readonly", "Readonly");
                TextBox13.Attributes.Add("Readonly", "Readonly");
                TextBox14.Attributes.Add("Readonly", "Readonly");


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
                com.CommandText = "select app_applcn_no from jpa_application where app_applcn_no like '%' + @Search + '%' and applcn_clsed ='N'";
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
                            countryNames.Add(sdr["app_applcn_no"].ToString());

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

    public string DecimalToWords(decimal number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + DecimalToWords(Math.Abs(number));

        string words = "";

        int intPortion = (int)number;
        decimal fraction = (number - intPortion) * 100;
        int decPortion = (int)fraction;

        words = NumberToWords(intPortion);
        if (decPortion > 0)
        {
            words += " dan ";
            words += NumberToWords(decPortion) + " Sen";
        }
        return words;
    }

    public static string NumberToWords(int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " Juta ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " Ribu ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " Ratus ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += " ";

            var unitsMap = new[] { "Sifar", "Satu", "Dua", "Tiga", "Empat", "Lima", "Enam", "Tujuh", "Lapan", "Sembilan", "Sepuluh", "Sebelas", "Dua Belas", "Tiga Belas", "Empat Belas", "Lima Belas", "Enam Belas", "Tujuh Belas", "Lapan Belas", "Sembilan Belas" };
            var tensMap = new[] { "Sifar", "Sepuluh", "Dua Puluh", "Tiga Puluh", "Empat Puluh", "Lima Puluh", "Enam Puluh", "Tujuh Puluh", "Lapan Puluh", "Sembilan Puluh" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += " " + unitsMap[number % 10];
            }
        }

        return words;
    }

    //public static string NumberToWords(int number)
    //{
    //    if (number == 0) return "Zero";

    //    if (number == -2147483648)
    //        return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";

    //    int[] num = new int[4];
    //    int first = 0;
    //    int u, h, t;
    //    System.Text.StringBuilder sb = new System.Text.StringBuilder();

    //    if (number < 0)
    //    {
    //        sb.Append("Minus ");
    //        number = -number;
    //    }

    //    string[] words0 = {"" ,"Satu ", "Dua ", "Tiga ", "Empat ","Lima " ,"Enam ", "Tujuh ", "Lapan ", "Sembilan "};

    //    string[] words1 = { "Sepuluh ", "Sebelas ", "Dua Belas ", "Tiga Belas ", "Empat Belas ", "Lima Belas ", "Enam Belas", "Tujuh Belas ", "Lapan Belas ", "Sembilan Belas " };

    //    string[] words2 = { "Dua Puluh ", "Tiga Puluh ", "Empat Puluh ", "Lima Puluh ", "Enam Puluh ", "Tujuh Puluh ", "Lapan Puluh ", "Sembilan Puluh " };

    //    string[] words3 = { "Ribu ", "Lakh ", "Crore " };

    //    num[0] = number % 1000; // units
    //    num[1] = number / 1000;
    //    num[2] = number / 100000;
    //    num[1] = num[1] - 100 * num[2]; // thousands
    //    num[3] = number / 10000000; // crores
    //    num[2] = num[2] - 100 * num[3]; // lakhs

    //    //You can increase as per your need.

    //    for (int i = 3; i > 0; i--)
    //    {
    //        if (num[i] != 0)
    //        {
    //            first = i;
    //            break;
    //        }
    //    }


    //    for (int i = first; i >= 0; i--)
    //    {
    //        if (num[i] == 0) continue;

    //        u = num[i] % 10; // ones
    //        t = num[i] / 10;
    //        h = num[i] / 100; // hundreds
    //        t = t - 10 * h; // tens

    //        if (h > 0) sb.Append(words0[h] + "Ratus ");

    //        if (u > 0 || t > 0)
    //        {
    //            if (h == 0)
    //                sb.Append("");
    //            else
    //                if (h > 0 || i == 0) sb.Append("dan ");


    //            if (t == 0)
    //                sb.Append(words0[u]);
    //            else if (t == 1)
    //                sb.Append(words1[u]);
    //            else
    //                sb.Append(words2[t - 2] + words0[u]);
    //        }

    //        if (i != 0) sb.Append(words3[i - 1]);

    //    }
    //    return sb.ToString().TrimEnd();
    //}


    protected void Searchbtn_Click(object sender, EventArgs e)
    {
        DataTable Dt = new DataTable();
        try
        {
            if (TXTNOKP.Text != "")
            {
                DataTable ddicno = new DataTable();
                ddicno = DBCon.Ora_Execute_table("select Distinct JA.app_applcn_no from jpa_application as JA Left join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no=JA.app_applcn_no Left Join jpa_guarantor as JG ON JG.gua_applcn_no=JA.app_applcn_no WHERE JA.app_applcn_no='" + TXTNOKP.Text + "'");
                if (ddicno.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Maklumat Carian Tidak Dijumpai');", true);
                }
                else
                {

                    string Select_App_query = "select JA.app_new_icno,JA.app_loan_amt,JA.appl_loan_dur,JA.app_name,JA.app_applcn_no,RW.Wilayah_Name,RB.branch_desc,RT.tujuan_desc from jpa_application as JA left join Ref_Wilayah as RW ON RW.Wilayah_Code=JA.app_region_cd left join ref_branch as RB ON RB.branch_cd=JA.app_branch_cd left join ref_tujuan as RT ON RT.tujuan_cd = JA.app_loan_purpose_cd where JA.app_applcn_no='" + TXTNOKP.Text + "'";
                    con.Open();
                    var sqlCommand = new SqlCommand(Select_App_query, con);
                    var sqlReader = sqlCommand.ExecuteReader();
                    if (sqlReader.Read() == true)
                    {
                        // Maklumat Pemohon
                        TextBox2.Text = sqlReader["app_name"].ToString();
                        //TextBox3.Text = sqlReader["Wilayah_Name"].ToString();
                        //TextBox4.Text = sqlReader["branch_desc"].ToString();
                        TextBox8.Text = sqlReader["tujuan_desc"].ToString();
                        decimal app_amt = (decimal)sqlReader["app_loan_amt"];
                        TextBox13.Text = app_amt.ToString("C").Replace("$","").Replace("RM", "");
                        TextBox14.Text = sqlReader["appl_loan_dur"].ToString();
                        DataTable mem_status = new DataTable();
                        mem_status = DBCon.Ora_Execute_table("select mem_staff_ind,mem_applicant_type_cd from mem_member where mem_new_icno ='" + sqlReader["app_new_icno"].ToString() + "'");
                        if (mem_status.Rows.Count != 0)
                        {
                            //if (mem_status.Rows[0]["mem_staff_ind"].ToString() == "Y")
                            //{
                            //    Button5.Visible = false;
                            //    Button6.Visible = false;
                            //    Button7.Visible = false;

                            //}
                            //else
                            //{
                            //    Button5.Visible = true;
                            //    Button6.Visible = true;
                            //    Button7.Visible = true;
                            //}

                            if (mem_status.Rows[0]["mem_applicant_type_cd"].ToString() == "LT")
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
                    }
                    sqlReader.Close();

                    DataTable ddicno_gua = new DataTable();
                    ddicno_gua = DBCon.Ora_Execute_table("select JG.*,RJP.Description as pengenalan,RN.Decription as mail_descript,RN1.Decription as per_descript from jpa_guarantor as JG left join Ref_Negeri as RN ON RN.Decription_Code=JG.gua_mailing_state_cd left join Ref_Negeri as RN1 ON RN1.Decription_Code=JG.gua_permanent_state_cd left join Ref_Jenis_Pengenalan as RJP ON RJP.Description_Code=JG.gua_ic_type_cd where jg.gua_applcn_no='" + TXTNOKP.Text + "' ORDER BY JG.gua_seq_no ASC");
                    int count = ddicno_gua.Rows.Count;
                    if (count != 0)
                    {

                        TextBox27.Text = ddicno_gua.Rows[0]["gua_name"].ToString();
                        TextBox35.Text = ddicno_gua.Rows[0]["gua_phone"].ToString();
                        TextArea7.Value = ddicno_gua.Rows[0]["gua_permanent_address"].ToString();
                        TextArea8.Value = ddicno_gua.Rows[0]["gua_mailing_address"].ToString();
                        TextBox62.Text = ddicno_gua.Rows[0]["gua_permanent_postcode"].ToString();
                        TextBox63.Text = ddicno_gua.Rows[0]["gua_mailing_postcode"].ToString();
                        DD_NegriBind5.Text = ddicno_gua.Rows[0]["per_descript"].ToString();
                        DD_NegriBind6.Text = ddicno_gua.Rows[0]["mail_descript"].ToString();
                        DD_Pengenalan2.Text = ddicno_gua.Rows[0]["pengenalan"].ToString();
                        
                        if (ddicno_gua.Rows[0]["gua_job_sector_ind"].ToString() != "")
                        {
                            ddcat.SelectedValue = ddicno_gua.Rows[0]["gua_job_sector_ind"].ToString();
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
                        TextBox31.Text = net_amt.ToString("0,0.00");
                        decimal gross_amt = (decimal)ddicno_gua.Rows[0]["gua_gross_income"];
                        TextBox33.Text = gross_amt.ToString("0,0.00");
                        TextBox34.Text = ddicno_gua.Rows[0]["gua_employer_name"].ToString();
                        TextBox30.Text = ddicno_gua.Rows[0]["gua_employer_phone"].ToString();
                        TextBox32.Text = ddicno_gua.Rows[0]["gua_employer_phone_ext"].ToString();

                        //2

                        if (count >= 2)
                        {
                            TextBox36.Text = ddicno_gua.Rows[1]["gua_name"].ToString();
                            TextBox37.Text = ddicno_gua.Rows[1]["gua_phone"].ToString();
                            TextArea5.Value = ddicno_gua.Rows[1]["gua_permanent_address"].ToString();
                            TextArea6.Value = ddicno_gua.Rows[1]["gua_mailing_address"].ToString();
                            TextBox60.Text = ddicno_gua.Rows[1]["gua_permanent_postcode"].ToString();
                            TextBox61.Text = ddicno_gua.Rows[1]["gua_mailing_postcode"].ToString();
                            DD_NegriBind7.Text = ddicno_gua.Rows[1]["per_descript"].ToString();
                            DD_NegriBind8.Text = ddicno_gua.Rows[1]["mail_descript"].ToString();
                            DD_Pengenalan3.Text = ddicno_gua.Rows[1]["pengenalan"].ToString();
                          
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
                            TextBox40.Text = net_amt1.ToString("0,0.00");
                            decimal gross_amt1 = (decimal)ddicno_gua.Rows[1]["gua_gross_income"];
                            TextBox41.Text = gross_amt1.ToString("0,0.00");

                            TextBox42.Text = ddicno_gua.Rows[1]["gua_employer_name"].ToString();
                            TextBox43.Text = ddicno_gua.Rows[1]["gua_employer_phone"].ToString();
                            TextBox44.Text = ddicno_gua.Rows[1]["gua_employer_phone_ext"].ToString();

                            //Button6.Visible = true;
                        }

                        //3
                        //if (count == 3)
                        //{
                        //    TextBox45.Text = ddicno_gua.Rows[2]["gua_name"].ToString();
                        //    TextBox46.Text = ddicno_gua.Rows[2]["gua_phone"].ToString();
                        //    TextArea9.Value = ddicno_gua.Rows[2]["gua_permanent_address"].ToString();
                        //    TextArea10.Value = ddicno_gua.Rows[2]["gua_mailing_address"].ToString();
                        //    TextBox58.Text = ddicno_gua.Rows[2]["gua_permanent_postcode"].ToString();
                        //    TextBox59.Text = ddicno_gua.Rows[2]["gua_mailing_postcode"].ToString();
                        //    DD_NegriBind9.Text = ddicno_gua.Rows[2]["per_descript"].ToString();
                        //    DD_NegriBind10.Text = ddicno_gua.Rows[2]["mail_descript"].ToString();
                        //    DD_Pengenalan4.Text = ddicno_gua.Rows[2]["pengenalan"].ToString();

                        //    if (ddicno_gua.Rows[2]["gua_job_sector_ind"].ToString() == "GOV")
                        //    {
                        //        RB31.Checked = true;
                        //    }
                        //    else if (ddicno_gua.Rows[2]["gua_job_sector_ind"].ToString() == "GLC")
                        //    {
                        //        RB32.Checked = true;
                        //    }
                        //    else if (ddicno_gua.Rows[2]["gua_job_sector_ind"].ToString() == "GLI")
                        //    {
                        //        RB33.Checked = true;
                        //    }
                        //    else
                        //    {
                        //        RB34.Checked = true;
                        //    }

                        //    if (ddicno_gua.Rows[2]["gua_job_subsidiary_ind"].ToString() == "1")
                        //    {
                        //        CheckBox2.Checked = true;
                        //    }
                        //    else
                        //    {
                        //        CheckBox2.Checked = false;
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
                        //    TextBox49.Text = net_amt2.ToString("0,0.00");
                        //    decimal gross_amt2 = (decimal)ddicno_gua.Rows[2]["gua_gross_income"];
                        //    TextBox50.Text = gross_amt2.ToString("0,0.00");

                        //    TextBox51.Text = ddicno_gua.Rows[2]["gua_employer_name"].ToString();
                        //    TextBox52.Text = ddicno_gua.Rows[2]["gua_employer_phone"].ToString();
                        //    TextBox53.Text = ddicno_gua.Rows[2]["gua_employer_phone_ext"].ToString();

                        //    //Button6.Visible = true;
                        //}

                    }

                }
            }
            else
            {
                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Permohonan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {
            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
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

    protected void update(object sender, EventArgs e)
    {

    }





    protected void print_offerletter(object sender, EventArgs e)
    {
        {
            try
            {
                if (TXTNOKP.Text != "")
                {
                    System.Web.UI.WebControls.Button button = (System.Web.UI.WebControls.Button)sender;
                    string buttonId = button.ID;
                    DataTable app_rujno = new DataTable();
                    app_rujno = DBCon.Ora_Execute_table("select cmn_applcn_no from cmn_ref_no where cmn_applcn_no= '" + TXTNOKP.Text + "' and cmn_ref_no='" + no_rujukan.Text + "'");
                    DataTable app_icno = new DataTable();
                    app_icno = DBCon.Ora_Execute_table("select app_new_icno from jpa_application where app_applcn_no= '" + TXTNOKP.Text + "'");
                    string sqno = string.Empty;
                    if (buttonId == "Button5")
                    {
                        sqno = "1";
                    }
                    else if (buttonId == "Button6")
                    {
                        sqno = "2";
                    }
                    else
                    {
                        sqno = "3";
                    }
                    if (app_rujno.Rows.Count != 0)
                    {
                        //Path
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        //if (buttonId == "Button5" || buttonId == "Button6" || buttonId == "Button7")
                        //{

                        //    dt = DBCon.Ora_Execute_table("select * from (select * from jpa_guarantor where gua_applcn_no='" + TXTNOKP.Text + "' and gua_seq_no='" + sqno + "') as a full outer join(select * from cmn_ref_no where cmn_applcn_no='" + TXTNOKP.Text + "' and cmn_crt_dt IN (SELECT max(cmn_crt_dt) FROM cmn_ref_no)) as b on b.cmn_applcn_no=a.gua_applcn_no full outer join(select * from jpa_application where app_applcn_no='" + TXTNOKP.Text + "') as c on c.app_applcn_no=b.cmn_applcn_no");
                        //}
                        //else
                        //{
                            dt = DBCon.Ora_Execute_table("select *,mm.mem_member_no,app_new_icno,FORMAT (GETDATE(), 'dd/MM/yyyy', 'en-US') as cdt,FORMAT (jkk.jkk_meeting_dt, 'dd/MM/yyyy', 'en-US') as jkk_mdt,ISNULL(db.pha_pay_amt,'0.00') as jb_amt,ISNULL(app_loan_amt,'')+ISNULL(app_cumm_profit_amt,'') as lon_prfit_tot from (select * from cmn_ref_no where cmn_crt_dt IN (SELECT max(cmn_crt_dt) FROM cmn_ref_no) and cmn_applcn_no='" + TXTNOKP.Text + "') as a full outer join (select * from jpa_application where app_applcn_no='" + TXTNOKP.Text + "') as b on b.app_applcn_no=a.cmn_applcn_no full outer join (select cal_applcn_no,isnull(cal_approve_amt,0)cal_approve_amt,isnull(cal_installment_amt,0)cal_installment_amt,isnull(cal_profit_amt,0)cal_profit_amt,isnull(cal_stamp_duty_amt,0)cal_stamp_duty_amt,isnull(cal_process_fee,0)cal_process_fee,isnull(cal_deposit_amt,0)cal_deposit_amt,isnull(cal_tkh_amt,0)cal_tkh_amt,(isnull(cal_approve_amt,0) + isnull(cal_profit_amt,0)) as ap_amt,isnull(cal_profit_rate,0)cal_profit_rate from jpa_calculate_fee where cal_applcn_no='" + TXTNOKP.Text + "') as c on c.cal_applcn_no=b.app_applcn_no left join jpa_jkkpa_approval jkk on jkk.jkk_applcn_no=b.app_applcn_no left join jpa_disburse db on db.pha_applcn_no=b.app_applcn_no left join mem_member mm on mm.mem_new_icno=b.app_new_icno");
                        //}

                        Rptviwer_cetak.Reset();
                        ds.Tables.Add(dt);

                        Rptviwer_cetak.LocalReport.DataSources.Clear();
                        ReportDataSource rds;
                        DataTable mem_status = new DataTable();
                        mem_status = DBCon.Ora_Execute_table("select app_loan_type_cd from jpa_application where app_applcn_no='" + TXTNOKP.Text + "'");
                        if (mem_status.Rows.Count != 0)
                        {
                            if (mem_status.Rows[0]["app_loan_type_cd"].ToString() == "P")
                            {

                                if (buttonId == "Button3")
                                {
                                    Rptviwer_cetak.LocalReport.ReportPath = "Pelaburan_Anggota/cetak_offer_sahabat.rdlc";
                                    rds = new ReportDataSource("coffer", dt);

                                }
                                //else if (buttonId == "Button4")
                                //{
                                //    Rptviwer_cetak.LocalReport.ReportPath = "Pelaburan_Anggota/Kemudahan_staff.rdlc";
                                //    rds = new ReportDataSource("kemudahan", dt);
                                //}
                                //else if (buttonId == "Button8")
                                //{
                                //    Rptviwer_cetak.LocalReport.ReportPath = "Pelaburan_Anggota/Tambahan.rdlc";
                                //    rds = new ReportDataSource("tambahan", dt);
                                //}
                                else if (buttonId == "Button4")
                                {
                                    Rptviwer_cetak.LocalReport.ReportPath = "Pelaburan_Anggota/Penjamin.rdlc";
                                    rds = new ReportDataSource("penjamin", dt);
                                }
                                else
                                {
                                    Rptviwer_cetak.LocalReport.ReportPath = "";
                                    rds = new ReportDataSource("coffer", dt);
                                }
                            }
                            else
                            {
                                if (buttonId == "Button3")
                                {
                                    Rptviwer_cetak.LocalReport.ReportPath = "Pelaburan_Anggota/cetak_offer_sahabat.rdlc";
                                    rds = new ReportDataSource("coffer", dt);

                                }
                                //else if (buttonId == "Button4")
                                //{
                                //    Rptviwer_cetak.LocalReport.ReportPath = "Pelaburan_Anggota/Kemudahan_sahabat.rdlc";
                                //    rds = new ReportDataSource("kemudahan", dt);
                                //}
                                //else if (buttonId == "Button8")
                                //{
                                //    Rptviwer_cetak.LocalReport.ReportPath = "Pelaburan_Anggota/Tambahan.rdlc";
                                //    rds = new ReportDataSource("tambahan", dt);
                                //}
                                else if (buttonId == "Button4")
                                {
                                    Rptviwer_cetak.LocalReport.ReportPath = "Pelaburan_Anggota/Penjamin.rdlc";
                                    rds = new ReportDataSource("penjamin", dt);
                                }
                                else
                                {
                                    Rptviwer_cetak.LocalReport.ReportPath = "";
                                    rds = new ReportDataSource("coffer", dt);
                                }
                            }
                        }
                        else
                        {
                            Rptviwer_cetak.LocalReport.ReportPath = "";
                            rds = new ReportDataSource("coffer", dt);
                        }
                        //Parameters
                     //   if (buttonId == "Button4")
                     //   {

                     //       decimal a10 = decimal.Parse(dt.Rows[0][40].ToString());
                     //       string ldu = DecimalToWords(a10);

                     //       decimal a4 = (decimal)dt.Rows[0]["cal_approve_amt"];
                     //       string cpa = DecimalToWords(a4);

                     //       decimal a5 = (decimal)dt.Rows[0]["cal_installment_amt"];
                     //       string cima = DecimalToWords(a5);

                     //       decimal a6 = (decimal)dt.Rows[0]["cal_profit_amt"];
                     //       string cpat = DecimalToWords(a6);

                     //       decimal a7 = (decimal)dt.Rows[0]["ap_amt"];
                     //       string apa = DecimalToWords(a7);

                     //       decimal a8 = (decimal)dt.Rows[0]["cal_profit_rate"];
                     //       string cpr = DecimalToWords(a8);

                     //       decimal a9 = (decimal)dt.Rows[0]["lon_prfit_tot"];
                     //       string lpt = DecimalToWords(a9);



                     //       ReportParameter[] rptParams = new ReportParameter[]{

                     //new ReportParameter("amt",  cpa ),
                     //new ReportParameter("amt1",  cima ),
                     //new ReportParameter("amt2",  cpat ),
                     //new ReportParameter("amt3",  apa ),
                     //new ReportParameter("amt4",  cpr ),
                     // new ReportParameter("amt5",  lpt ),
                     //new ReportParameter("amt6",  ldu )

                     //};


                     //       Rptviwer_cetak.LocalReport.SetParameters(rptParams);
                     //   }
                        Rptviwer_cetak.LocalReport.DataSources.Add(rds);

                        //Refresh
                        Rptviwer_cetak.LocalReport.Refresh();
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

                        byte[] bytes = Rptviwer_cetak.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                        Response.Buffer = true;

                        Response.Clear();

                        Response.ClearHeaders();

                        Response.ClearContent();

                        Response.ContentType = "application/pdf";

                        if (buttonId == "Button3")
                        {
                            Response.AddHeader("content-disposition", "attachment; filename=Surat_Tawaran_" + TXTNOKP.Text + "." + extension);
                        }
                        //else if (buttonId == "Button8")
                        //{
                        //    Response.AddHeader("content-disposition", "attachment; filename=Perjanjian_Tambahan_" + TXTNOKP.Text + "." + extension);
                        //}
                        else if (buttonId == "Button4")
                        {
                            Response.AddHeader("content-disposition", "attachment; filename=Kemudahan_Perjanjian_" + TXTNOKP.Text + "." + extension);
                        }
                        else
                        {
                            Response.AddHeader("content-disposition", "attachment; filename=Kemudahan_Perjanjian_" + TXTNOKP.Text + "." + extension);
                        }

                        Response.BinaryWrite(bytes);

                        //Response.Write("<script>");
                        //Response.Write("window.open('', '_newtab');");
                        //Response.Write("</script>");
                        Response.Flush();

                        Response.End();
                    }

                    else
                    {
                        tab1();
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan No Rujukan',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    tab1();
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    protected void Click_rujukan(object sender, EventArgs e)
    {
        try
        {

            if (TXTNOKP.Text != "")
            {
                DataTable app_rujno = new DataTable();
                app_rujno = DBCon.Ora_Execute_table("select cmn_applcn_no from cmn_ref_no where cmn_applcn_no= '" + TXTNOKP.Text + "' and cmn_ref_no='" + no_rujukan.Text + "'");
                if (app_rujno.Rows.Count == 0)
                {
                    DBCon.Execute_CommamdText("INSERT into cmn_ref_no (cmn_applcn_no,cmn_ref_no,cmn_txn_dt,cmn_txn_cd,cmn_crt_id,cmn_crt_dt) values ('" + TXTNOKP.Text + "','" + no_rujukan.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','','" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')");
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Berjaya Disimpan',{'type': 'confirmation','title': 'Success','auto_close': 2000});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('No Rujukan Telah Wujud.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

        }
    }
}