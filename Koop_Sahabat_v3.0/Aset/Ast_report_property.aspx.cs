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

public partial class Ast_report_property : System.Web.UI.Page
{
    
    DBConnection Dblog = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection Con = new DBConnection();
    StudentWebService service = new StudentWebService();
    DataTable dt = new DataTable();

    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty, st_qry = string.Empty;
    string clmfd = string.Empty, clm_name = string.Empty;
    string ss1 = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string script = " $(function () {$('.select2').select2()})";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                var samp = Request.Url.Query;
                Negri();
                useid = Session["New"].ToString();
               
             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    void Negri()
    {
        DataSet Ds = new DataSet();
        try
        {
            DataTable dd_negri = new DataTable();
            dd_negri = Con.Ora_Execute_table("SELECT STUFF ((SELECT ',' + pro_state_cd  FROM ast_property FOR XML PATH ('')  ),1,1,'')  as scode");

            string ss1 = string.Empty;
            if (dd_negri.Rows[0]["scode"].ToString() != "")
            {
                ss1 = dd_negri.Rows[0]["scode"].ToString();
            }
            else
            {
                ss1 = "0";
            }

            string com = "select hr_negeri_Code,hr_negeri_desc from Ref_hr_negeri where hr_negeri_Code IN (" + ss1 + ") and Status='A' order by hr_negeri_desc ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ss_dd.DataSource = dt;
            ss_dd.DataTextField = "hr_negeri_desc";
            ss_dd.DataValueField = "hr_negeri_Code";
            ss_dd.DataBind();
            ss_dd.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void shw_negri(object sender, EventArgs e)
    //{
    //    if (cb1.Checked == true)
    //    {
    //        sw_neg.Attributes.Remove("style");
    //    }
    //    else if (cb1.Checked == false)
    //    {
    //        sw_neg.Attributes.Add("style","display: None;");
    //    }
    //    Negri();
    //}

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
                using (DataSet dsCustomers = new DataSet())
                {
                    sda.Fill(dsCustomers, "DataTable1");
                    return dsCustomers;
                }
            }
        }
    }

 
    protected void Button4_Click(object sender, EventArgs e)
    {
        try
        {
            Page.Header.Title = "PILIHAN JANAAN LAPORAN";
            if ((txticno.Text != ""))
            {

                if (chk_all.Checked == false)
                {
                    //DataSet dsCustomers = GetData("select  "+ +",pro_state_cd,pro_city,pro_lot_no,pro_mtr_sqft,pro_usage_cd,pro_loan_amt,pro_installment_amt,pro_loan_dur,pro_interest_rate,pro_bank_cd,pro_hold_type_ind,pro_lease_dur,pro_lease_due_dt,pro_address,ren_rental_amt,pro_lease_ind,pro_ownership_no,pro_file_no,pro_constraint,pro_assessment_due_dt,pro_assessment_amt,pro_land_tax_due_dt,pro_land_tax_amt,pro_city_tax_due_dt,pro_city_tax_amt,pro_ownership_cd,pro_ownership_prcntg,pro_agent_id,ren_comp_name,ren_start_dt,ren_end_dt From ast_property as p left join ast_rental as r on r.ren_ownership_no=p.pro_ownership_no and r.ren_state_cd=p.pro_state_cd");

                    //dt = dsCustomers.Tables[0];

                    int count = 0;
                    if (cb1.Checked) { count++; }
                    if (cb2.Checked) { count++; }
                    if (cb3.Checked) { count++; }
                    if (cb4.Checked) { count++; }
                    if (cb5.Checked) { count++; }
                    if (cb6.Checked) { count++; }
                    if (cb7.Checked) { count++; }
                    if (cb8.Checked) { count++; }
                    if (cb9.Checked) { count++; }
                    if (cb10.Checked) { count++; }
                    if (cb11.Checked) { count++; }
                    if (cb12.Checked) { count++; }
                    if (cb13.Checked) { count++; }
                    if (cb14.Checked) { count++; }
                    if (cb15.Checked) { count++; }
                    if (cb16.Checked) { count++; }
                    if (cb17.Checked) { count++; }
                    if (cb18.Checked) { count++; }
                    if (cb19.Checked) { count++; }
                    if (cb20.Checked) { count++; }
                    if (cb21.Checked) { count++; }
                    if (cb22.Checked) { count++; }
                    if (cb23.Checked) { count++; }
                    if (cb24.Checked) { count++; }
                    if (cb25.Checked) { count++; }
                    if (cb26.Checked) { count++; }
                    if (cb27.Checked) { count++; }
                    if (cb28.Checked) { count++; }
                    if (cb29.Checked) { count++; }
                    if (cb30.Checked) { count++; }


                    if (count == 1)
                    {

                        string sb1, sb2, sb3, sb4, sb5, sb6, sb7, sb8, sb9, sb10, sb11, sb12, sb13, sb14, sb15, sb16, sb17, sb18, sb19, sb20, sb21, sb22, sb23, sb24, sb25, sb26, sb27, sb28, sb29, sb30;
                        string coma = ",";
                        string notcoma = "";
                        if ((cb1.Checked == true))
                        {
                            sb1 = ("n.Decription as pro_state_cd");
                        }
                        else
                        {
                            sb1 = ("");
                        }
                        string values1 = sb1.ToString();


                        if ((cb2.Checked == true))
                        {
                            sb2 = ("pro_lease_ind");
                        }
                        else
                        {
                            sb2 = ("");
                        }
                        string values2 = sb2.ToString();


                        if ((cb3.Checked == true))
                        {
                            sb3 = ("pro_city");
                        }
                        else
                        {
                            sb3 = ("");
                        }
                        string values3 = sb3.ToString();

                        if ((cb4.Checked == true))
                        {
                            sb4 = ("pro_ownership_no");
                        }
                        else
                        {
                            sb4 = ("");
                        }
                        string values4 = sb4.ToString();


                        if ((cb5.Checked == true))
                        {
                            sb5 = ("pro_lot_no");
                        }
                        else
                        {
                            sb5 = ("");
                        }
                        string values5 = sb5.ToString();


                        if ((cb6.Checked == true))
                        {
                            sb6 = ("pro_file_no");
                        }
                        else
                        {
                            sb6 = ("");
                        }
                        string values6 = sb6.ToString();


                        if ((cb7.Checked == true))
                        {
                            sb7 = ("pro_mtr_sqft");
                        }
                        else
                        {
                            sb7 = ("");
                        }
                        string values7 = sb7.ToString();


                        if ((cb8.Checked == true))
                        {
                            sb8 = ("pro_constraint");
                        }
                        else
                        {
                            sb8 = ("");
                        }
                        string values8 = sb8.ToString();


                        if ((cb9.Checked == true))
                        {
                            sb9 = ("UPPER(ast_Penggunaan_desc) as pro_usage_cd");
                        }
                        else
                        {
                            sb9 = ("");
                        }
                        string values9 = sb9.ToString();


                        if ((cb10.Checked == true))
                        {
                            sb10 = ("Isnull(pro_assessment_due_dt,'') as pro_assessment_due_dt");
                        }
                        else
                        {
                            sb10 = ("");
                        }
                        string values10 = sb10.ToString();


                        if ((cb11.Checked == true))
                        {
                            sb11 = ("pro_loan_amt");
                        }
                        else
                        {
                            sb11 = ("");
                        }
                        string values11 = sb11.ToString();


                        if ((cb12.Checked == true))
                        {
                            sb12 = ("pro_assessment_amt");
                        }
                        else
                        {
                            sb12 = ("");
                        }
                        string values12 = sb12.ToString();


                        if ((cb13.Checked == true))
                        {
                            sb13 = ("pro_installment_amt");
                        }
                        else
                        {
                            sb13 = ("");
                        }
                        string values13 = sb13.ToString();

                        if ((cb14.Checked == true))
                        {
                            sb14 = ("ISnull(pro_land_tax_due_dt,'') as pro_land_tax_due_dt");
                        }
                        else
                        {
                            sb14 = ("");
                        }
                        string values14 = sb14.ToString();


                        if ((cb15.Checked == true))
                        {
                            sb15 = ("pro_loan_dur");
                        }
                        else
                        {
                            sb15 = ("");
                        }
                        string values15 = sb15.ToString();


                        if ((cb16.Checked == true))
                        {
                            sb16 = ("pro_land_tax_amt");
                        }
                        else
                        {
                            sb16 = ("");
                        }
                        string values16 = sb16.ToString();


                        if ((cb17.Checked == true))
                        {
                            sb17 = ("pro_interest_rate");
                        }
                        else
                        {
                            sb17 = ("");
                        }
                        string values17 = sb17.ToString();


                        if ((cb18.Checked == true))
                        {
                            sb18 = ("FORMAT(pro_city_tax_due_dt,'dd/MM/yyyy', 'en-us') pro_city_tax_due_dt");
                        }
                        else
                        {
                            sb18 = ("");
                        }
                        string values18 = sb18.ToString();


                        if ((cb19.Checked == true))
                        {
                            sb19 = ("UPPER(Bank_Name) as pro_bank_cd");
                        }
                        else
                        {
                            sb19 = ("");
                        }
                        string values19 = sb19.ToString();


                        if ((cb20.Checked == true))
                        {
                            sb20 = ("pro_city_tax_amt");
                        }
                        else
                        {
                            sb20 = ("");
                        }
                        string values20 = sb20.ToString();


                        if ((cb21.Checked == true))
                        {
                            sb21 = ("pro_hold_type_ind");
                        }
                        else
                        {
                            sb21 = ("");
                        }
                        string values21 = sb21.ToString();


                        if ((cb22.Checked == true))
                        {
                            sb22 = ("Upper(ast_Milikan_desc) as pro_ownership_cd");
                        }
                        else
                        {
                            sb22 = ("");
                        }
                        string values22 = sb22.ToString();


                        if ((cb23.Checked == true))
                        {
                            sb23 = ("pro_lease_dur");
                        }
                        else
                        {
                            sb23 = ("");
                        }
                        string values23 = sb23.ToString();

                        if ((cb24.Checked == true))
                        {
                            sb24 = ("pro_ownership_prcntg");
                        }
                        else
                        {
                            sb24 = ("");
                        }
                        string values24 = sb24.ToString();


                        if ((cb25.Checked == true))
                        {
                            sb25 = ("FORMAT(pro_lease_due_dt,'dd/MM/yyyy', 'en-us')  pro_lease_due_dt");
                        }
                        else
                        {
                            sb25 = ("");
                        }
                        string values25 = sb25.ToString();


                        if ((cb26.Checked == true))
                        {
                            sb26 = ("pro_agent_id");
                        }
                        else
                        {
                            sb26 = ("");
                        }
                        string values26 = sb26.ToString();


                        if ((cb27.Checked == true))
                        {
                            sb27 = ("pro_address");
                        }
                        else
                        {
                            sb27 = ("");
                        }
                        string values27 = sb27.ToString();


                        if ((cb28.Checked == true))
                        {
                            sb28 = ("ren_comp_name");
                        }
                        else
                        {
                            sb28 = ("");
                        }
                        string values28 = sb28.ToString();


                        if ((cb29.Checked == true))
                        {
                            sb29 = ("ren_rental_amt");
                        }
                        else
                        {
                            sb29 = ("");
                        }
                        string values29 = sb29.ToString();


                        if ((cb30.Checked == true))
                        {
                            //sb30 = ("Isnull(ren_start_dt,'') as ren_start_dt") + coma + ("Isnull(ren_end_dt,'') as ren_end_dt");
                            sb30 = ("FORMAT(ren_start_dt,'dd/MM/yyyy', 'en-us') ren_start_dt") + coma + ("FORMAT(ren_end_dt,'dd/MM/yyyy', 'en-us') ren_end_dt");
                        }
                        else
                        {
                            sb30 = ("");
                        }
                        string values30 = sb30.ToString();


                        if (ss_dd.SelectedValue != "")
                        {
                            DataSet dsCustomers = GetData("select " + values1 + "" + values2 + "" + values3 + "" + values4 + "" + values5 + "" + values6 + "" + values7 + "" + values8 + "" + values9 + "" + values10 + "" + values11 + "" + values12 + "" + values13 + "" + values14 + "" + values15 + "" + values16 + "" + values17 + "" + values18 + "" + values19 + "" + values20 + "" + values21 + "" + values22 + "" + values23 + "" + values24 + "" + values25 + "" + values26 + "" + values27 + "" + values28 + "" + values29 + "" + values30 + " From ast_property as p left join ast_rental as r on r.ren_ownership_no=p.pro_ownership_no and r.ren_state_cd=p.pro_state_cd left join Ref_Negeri as n on n.Decription_Code=p.pro_state_cd left join Ref_Nama_Bank as b on b.Bank_Code=p.pro_bank_cd left join Ref_ast_Jenis_Milikan as jm on jm.ast_Milikan_code=pro_ownership_cd left join Ref_ast_Penggunaan_Tanah as pt on ast_Penggunaan_code=pro_usage_cd where pro_state_cd='" + ss_dd.SelectedValue + "'");
                            dt = dsCustomers.Tables[0];
                        }
                        else
                        {
                            DataSet dsCustomers = GetData("select " + values1 + "" + values2 + "" + values3 + "" + values4 + "" + values5 + "" + values6 + "" + values7 + "" + values8 + "" + values9 + "" + values10 + "" + values11 + "" + values12 + "" + values13 + "" + values14 + "" + values15 + "" + values16 + "" + values17 + "" + values18 + "" + values19 + "" + values20 + "" + values21 + "" + values22 + "" + values23 + "" + values24 + "" + values25 + "" + values26 + "" + values27 + "" + values28 + "" + values29 + "" + values30 + " From ast_property as p left join ast_rental as r on r.ren_ownership_no=p.pro_ownership_no and r.ren_state_cd=p.pro_state_cd left join Ref_Negeri as n on n.Decription_Code=p.pro_state_cd left join Ref_Nama_Bank as b on b.Bank_Code=p.pro_bank_cd left join Ref_ast_Jenis_Milikan as jm on jm.ast_Milikan_code=pro_ownership_cd left join Ref_ast_Penggunaan_Tanah as pt on ast_Penggunaan_code=pro_usage_cd");
                            dt = dsCustomers.Tables[0];
                        }


                    }
                    else if (count > 1)
                    {

                        string sb1, sb2, sb3, sb4, sb5, sb6, sb7, sb8, sb9, sb10, sb11, sb12, sb13, sb14, sb15, sb16, sb17, sb18, sb19, sb20, sb21, sb22, sb23, sb24, sb25, sb26, sb27, sb28, sb29, sb30;
                        string coma = ",";
                        string notcoma = "";
                        if ((cb1.Checked == true))
                        {
                            sb1 = ("n.Decription as pro_state_cd");
                        }
                        else
                        {
                            sb1 = ("");
                        }
                        string values1 = sb1.ToString();


                        if ((cb2.Checked == true))
                        {
                            sb2 = coma + ("pro_lease_ind");
                        }
                        else
                        {
                            sb2 = ("");
                        }
                        string values2 = sb2.ToString();


                        if ((cb3.Checked == true))
                        {
                            sb3 = coma + ("pro_city");
                        }
                        else
                        {
                            sb3 = ("");
                        }
                        string values3 = sb3.ToString();

                        if ((cb4.Checked == true))
                        {
                            sb4 = coma + ("pro_ownership_no");
                        }
                        else
                        {
                            sb4 = ("");
                        }
                        string values4 = sb4.ToString();


                        if ((cb5.Checked == true))
                        {
                            sb5 = coma + ("pro_lot_no");
                        }
                        else
                        {
                            sb5 = ("");
                        }
                        string values5 = sb5.ToString();


                        if ((cb6.Checked == true))
                        {
                            sb6 = coma + ("pro_file_no");
                        }
                        else
                        {
                            sb6 = ("");
                        }
                        string values6 = sb6.ToString();


                        if ((cb7.Checked == true))
                        {
                            sb7 = coma + ("pro_mtr_sqft");
                        }
                        else
                        {
                            sb7 = ("");
                        }
                        string values7 = sb7.ToString();


                        if ((cb8.Checked == true))
                        {
                            sb8 = coma + ("pro_constraint");
                        }
                        else
                        {
                            sb8 = ("");
                        }
                        string values8 = sb8.ToString();


                        if ((cb9.Checked == true))
                        {
                            sb9 = coma + ("UPPER(ast_Penggunaan_desc) as pro_usage_cd");
                        }
                        else
                        {
                            sb9 = ("");
                        }
                        string values9 = sb9.ToString();


                        if ((cb10.Checked == true))
                        {
                            sb10 = coma + ("FORMAT(pro_assessment_due_dt,'dd/MM/yyyy', 'en-us') pro_assessment_due_dt");
                        }
                        else
                        {
                            sb10 = ("");
                        }
                        string values10 = sb10.ToString();


                        if ((cb11.Checked == true))
                        {
                            sb11 = coma + ("pro_loan_amt");
                        }
                        else
                        {
                            sb11 = ("");
                        }
                        string values11 = sb11.ToString();


                        if ((cb12.Checked == true))
                        {
                            sb12 = coma + ("pro_assessment_amt");
                        }
                        else
                        {
                            sb12 = ("");
                        }
                        string values12 = sb12.ToString();


                        if ((cb13.Checked == true))
                        {
                            sb13 = coma + ("pro_installment_amt");
                        }
                        else
                        {
                            sb13 = ("");
                        }
                        string values13 = sb13.ToString();

                        if ((cb14.Checked == true))
                        {
                            sb14 = coma + ("FORMAT(pro_land_tax_due_dt,'dd/MM/yyyy', 'en-us') pro_land_tax_due_dt");
                        }
                        else
                        {
                            sb14 = ("");
                        }
                        string values14 = sb14.ToString();


                        if ((cb15.Checked == true))
                        {
                            sb15 = coma + ("pro_loan_dur");
                        }
                        else
                        {
                            sb15 = ("");
                        }
                        string values15 = sb15.ToString();


                        if ((cb16.Checked == true))
                        {
                            sb16 = coma + ("pro_land_tax_amt");
                        }
                        else
                        {
                            sb16 = ("");
                        }
                        string values16 = sb16.ToString();


                        if ((cb17.Checked == true))
                        {
                            sb17 = coma + ("pro_interest_rate");
                        }
                        else
                        {
                            sb17 = ("");
                        }
                        string values17 = sb17.ToString();


                        if ((cb18.Checked == true))
                        {
                            sb18 = coma + ("pro_city_tax_due_dt");
                        }
                        else
                        {
                            sb18 = ("");
                        }
                        string values18 = sb18.ToString();


                        if ((cb19.Checked == true))
                        {
                            sb19 = coma + ("UPPER(Bank_Name) as pro_bank_cd");
                        }
                        else
                        {
                            sb19 = ("");
                        }
                        string values19 = sb19.ToString();


                        if ((cb20.Checked == true))
                        {
                            sb20 = coma + ("pro_city_tax_amt");
                        }
                        else
                        {
                            sb20 = ("");
                        }
                        string values20 = sb20.ToString();


                        if ((cb21.Checked == true))
                        {
                            sb21 = coma + ("pro_hold_type_ind");
                        }
                        else
                        {
                            sb21 = ("");
                        }
                        string values21 = sb21.ToString();


                        if ((cb22.Checked == true))
                        {
                            sb22 = coma + ("Upper(ast_Milikan_desc) as pro_ownership_cd");
                        }
                        else
                        {
                            sb22 = ("");
                        }
                        string values22 = sb22.ToString();


                        if ((cb23.Checked == true))
                        {
                            sb23 = coma + ("pro_lease_dur");
                        }
                        else
                        {
                            sb23 = ("");
                        }
                        string values23 = sb23.ToString();

                        if ((cb24.Checked == true))
                        {
                            sb24 = coma + ("pro_ownership_prcntg");
                        }
                        else
                        {
                            sb24 = ("");
                        }
                        string values24 = sb24.ToString();


                        if ((cb25.Checked == true))
                        {
                            sb25 = coma + ("pro_lease_due_dt");
                        }
                        else
                        {
                            sb25 = ("");
                        }
                        string values25 = sb25.ToString();


                        if ((cb26.Checked == true))
                        {
                            sb26 = coma + ("pro_agent_id");
                        }
                        else
                        {
                            sb26 = ("");
                        }
                        string values26 = sb26.ToString();


                        if ((cb27.Checked == true))
                        {
                            sb27 = coma + ("pro_address");
                        }
                        else
                        {
                            sb27 = ("");
                        }
                        string values27 = sb27.ToString();


                        if ((cb28.Checked == true))
                        {
                            sb28 = coma + ("ren_comp_name");
                        }
                        else
                        {
                            sb28 = ("");
                        }
                        string values28 = sb28.ToString();


                        if ((cb29.Checked == true))
                        {
                            sb29 = coma + ("ren_rental_amt");
                        }
                        else
                        {
                            sb29 = ("");
                        }
                        string values29 = sb29.ToString();


                        if ((cb30.Checked == true))
                        {
                            sb30 = coma + ("FORMAT(ren_start_dt,'dd/MM/yyyy', 'en-us') ren_start_dt") + coma + ("FORMAT(ren_end_dt,'dd/MM/yyyy', 'en-us') ren_end_dt");
                        }
                        else
                        {
                            sb30 = ("");
                        }
                        string values30 = sb30.ToString();


                        if (ss_dd.SelectedValue != "")
                        {
                            DataSet dsCustomers = GetData("select " + values1 + "" + values2 + "" + values3 + "" + values4 + "" + values5 + "" + values6 + "" + values7 + "" + values8 + "" + values9 + "" + values10 + "" + values11 + "" + values12 + "" + values13 + "" + values14 + "" + values15 + "" + values16 + "" + values17 + "" + values18 + "" + values19 + "" + values20 + "" + values21 + "" + values22 + "" + values23 + "" + values24 + "" + values25 + "" + values26 + "" + values27 + "" + values28 + "" + values29 + "" + values30 + " From ast_property as p left join ast_rental as r on r.ren_ownership_no=p.pro_ownership_no and r.ren_state_cd=p.pro_state_cd left join Ref_Negeri as n on n.Decription_Code=p.pro_state_cd left join Ref_Nama_Bank as b on b.Bank_Code=p.pro_bank_cd left join Ref_ast_Jenis_Milikan as jm on jm.ast_Milikan_code=pro_ownership_cd left join Ref_ast_Penggunaan_Tanah as pt on ast_Penggunaan_code=pro_usage_cd where pro_state_cd='" + ss_dd.SelectedValue + "'");
                            dt = dsCustomers.Tables[0];
                        }
                        else
                        {
                            DataSet dsCustomers = GetData("select " + values1 + "" + values2 + "" + values3 + "" + values4 + "" + values5 + "" + values6 + "" + values7 + "" + values8 + "" + values9 + "" + values10 + "" + values11 + "" + values12 + "" + values13 + "" + values14 + "" + values15 + "" + values16 + "" + values17 + "" + values18 + "" + values19 + "" + values20 + "" + values21 + "" + values22 + "" + values23 + "" + values24 + "" + values25 + "" + values26 + "" + values27 + "" + values28 + "" + values29 + "" + values30 + " From ast_property as p left join ast_rental as r on r.ren_ownership_no=p.pro_ownership_no and r.ren_state_cd=p.pro_state_cd left join Ref_Negeri as n on n.Decription_Code=p.pro_state_cd left join Ref_Nama_Bank as b on b.Bank_Code=p.pro_bank_cd left join Ref_ast_Jenis_Milikan as jm on jm.ast_Milikan_code=pro_ownership_cd left join Ref_ast_Penggunaan_Tanah as pt on ast_Penggunaan_code=pro_usage_cd");
                            dt = dsCustomers.Tables[0];
                        }


                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Sila pilih kotak satu cek Minimum.');", true);
                        //DataSet dsCustomers = GetData("select pro_state_cd,pro_city,pro_lot_no,pro_mtr_sqft,pro_usage_cd,pro_loan_amt,pro_installment_amt,pro_loan_dur,pro_interest_rate,pro_bank_cd,pro_hold_type_ind,pro_lease_dur,pro_lease_due_dt,pro_address,ren_rental_amt,pro_lease_ind,pro_ownership_no,pro_file_no,pro_constraint,pro_assessment_due_dt,pro_assessment_amt,pro_land_tax_due_dt,pro_land_tax_amt,pro_city_tax_due_dt,pro_city_tax_amt,pro_ownership_cd,pro_ownership_prcntg,pro_agent_id,ren_comp_name,ren_start_dt,ren_end_dt From ast_property as p left join ast_rental as r on r.ren_ownership_no=p.pro_ownership_no and r.ren_state_cd=p.pro_state_cd");
                        //dt = dsCustomers.Tables[0];
                    }
                }
                else
                {
                    if (ss_dd.SelectedValue != "")
                    {
                        DataSet dsCustomers = GetData("select n.Decription as pro_state_cd,pro_lease_ind,pro_city,pro_ownership_no,pro_lot_no,pro_file_no,pro_mtr_sqft,pro_constraint,UPPER(ast_Penggunaan_desc) as pro_usage_cd,Isnull(FORMAT(pro_assessment_due_dt,'dd/MM/yyyy', 'en-us'),'') as pro_assessment_due_dt,pro_loan_amt,pro_assessment_amt,pro_installment_amt,ISnull(FORMAT(pro_land_tax_due_dt,'dd/MM/yyyy', 'en-us'),'') as pro_land_tax_due_dt,pro_loan_dur,pro_land_tax_amt,pro_interest_rate,FORMAT(pro_city_tax_due_dt,'dd/MM/yyyy', 'en-us') pro_city_tax_due_dt,UPPER(Bank_Name) as pro_bank_cd,pro_city_tax_amt,pro_hold_type_ind,Upper(ast_Milikan_desc) as pro_ownership_cd,pro_lease_dur,pro_ownership_prcntg,FORMAT(pro_lease_due_dt,'dd/MM/yyyy', 'en-us')  pro_lease_due_dt,pro_agent_id,pro_address,ren_comp_name,ren_rental_amt,Isnull(FORMAT(ren_start_dt,'dd/MM/yyyy', 'en-us'),'') as ren_start_dt,Isnull(FORMAT(ren_end_dt,'dd/MM/yyyy', 'en-us'),'') as ren_end_dt From ast_property as p left join ast_rental as r on r.ren_ownership_no=p.pro_ownership_no and r.ren_state_cd=p.pro_state_cd left join Ref_Negeri as n on n.Decription_Code=p.pro_state_cd left join Ref_Nama_Bank as b on b.Bank_Code=p.pro_bank_cd left join Ref_ast_Jenis_Milikan as jm on jm.ast_Milikan_code=pro_ownership_cd left join Ref_ast_Penggunaan_Tanah as pt on ast_Penggunaan_code=pro_usage_cd where pro_state_cd='" + ss_dd.SelectedValue + "'");
                        dt = dsCustomers.Tables[0];
                    }
                    else
                    {
                        DataSet dsCustomers = GetData("select n.Decription as pro_state_cd,pro_lease_ind,pro_city,pro_ownership_no,pro_lot_no,pro_file_no,pro_mtr_sqft,pro_constraint,UPPER(ast_Penggunaan_desc) as pro_usage_cd,Isnull(FORMAT(pro_assessment_due_dt,'dd/MM/yyyy', 'en-us'),'') as pro_assessment_due_dt,pro_loan_amt,pro_assessment_amt,pro_installment_amt,ISnull(FORMAT(pro_land_tax_due_dt,'dd/MM/yyyy', 'en-us'),'') as pro_land_tax_due_dt,pro_loan_dur,pro_land_tax_amt,pro_interest_rate,FORMAT(pro_city_tax_due_dt,'dd/MM/yyyy', 'en-us') pro_city_tax_due_dt,UPPER(Bank_Name) as pro_bank_cd,pro_city_tax_amt,pro_hold_type_ind,Upper(ast_Milikan_desc) as pro_ownership_cd,pro_lease_dur,pro_ownership_prcntg,FORMAT(pro_lease_due_dt,'dd/MM/yyyy', 'en-us')  pro_lease_due_dt,pro_agent_id,pro_address,ren_comp_name,ren_rental_amt,Isnull(FORMAT(ren_start_dt,'dd/MM/yyyy', 'en-us'),'') as ren_start_dt,Isnull(FORMAT(ren_end_dt,'dd/MM/yyyy', 'en-us'),'') as ren_end_dt From ast_property as p left join ast_rental as r on r.ren_ownership_no=p.pro_ownership_no and r.ren_state_cd=p.pro_state_cd left join Ref_Negeri as n on n.Decription_Code=p.pro_state_cd left join Ref_Nama_Bank as b on b.Bank_Code=p.pro_bank_cd left join Ref_ast_Jenis_Milikan as jm on jm.ast_Milikan_code=pro_ownership_cd left join Ref_ast_Penggunaan_Tanah as pt on ast_Penggunaan_code=pro_usage_cd");
                        dt = dsCustomers.Tables[0];
                    }
                }
                ReportViewer1.Reset();

                List<DataRow> listResult = dt.AsEnumerable().ToList();
                listResult.Count();
                int countRow = 0;
                countRow = listResult.Count();

                if (countRow != 0)
                {
                    disp_hdr_txt.Visible = true;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("AST_RPT_PROPRTY", dt);
                    //Path
                    ReportViewer1.LocalReport.ReportPath = "Aset/AST_RPT_PRPOPERTY_1.rdlc";
                    //Parameters
                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("headtext",txticno.Text),

                     };

                    ReportViewer1.LocalReport.DataSources.Add(rds);
                    ReportViewer1.LocalReport.SetParameters(rptParams);
                    ReportViewer1.LocalReport.DisplayName = "JANAAN_LAPORAN_" + DateTime.Now.ToString("ddMMyyyy");
                    //Refresh
                    ReportViewer1.LocalReport.Refresh();

                    //Warning[] warnings;

                    //string[] streamids;

                    //string mimeType;

                    //string encoding;

                    //string extension;
                    //string filename;

                    //filename = string.Format("{0}.{1}", "Laporan_Hartanah_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                    //byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                    ////byte[] bytes = ReportViewer1.LocalReport.Render("PDF", devinfo, out mimeType, out encoding, out extension, out streamids, out warnings);


                    //Response.Buffer = true;
                    //Response.Clear();
                    //Response.ContentType = mimeType;
                    //string extfile = DateTime.Now.ToString("dd_MM_yyyy.");
                    //Response.AddHeader("content-disposition", "inline; filename=AST_RPT_PROPERTY" + extfile + extension);
                    //Response.BinaryWrite(bytes);
                    //Response.Flush();
                    //Response.End();

                }
                else if (countRow == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'warning','auto_close': 2000});", true);
            }
        }


        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();

        }
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Session["validate_success"] = "";
        Session["alrt_msg"] = "";
        Response.Redirect("../Aset/Ast_report_property.aspx");
    }

 
    
}