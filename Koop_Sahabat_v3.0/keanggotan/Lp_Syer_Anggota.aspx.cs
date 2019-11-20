using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Net;
using System.Threading;

public partial class keanggotan_Lp_Syer_Anggota : System.Web.UI.Page
{
    DBConnection dbcon = new DBConnection();
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable wilayah = new DataTable();
    DataTable caw = new DataTable();
    StudentWebService service = new StudentWebService();
    DataTable pusat = new DataTable();
    DataTable dt = new DataTable();
    DataTable ddtt = new DataTable();
    DataTable Mpcheck = new DataTable();
    DataTable MpWil = new DataTable();
    DataTable MpWila = new DataTable();
    DBConnection DBCon = new DBConnection();
    DataSet ds = new DataSet();
    string level, userid;
    string filename = string.Empty;
    string ss1 = string.Empty, ss2 = string.Empty, ss3 = string.Empty, ss4 = string.Empty, ss5 = string.Empty, rdlc_name = string.Empty, DS_name = string.Empty;
    string status;
    DateTime dmula;
    string bcode;
    string wcode;
    string sno;
    string ccode;
    string Status = string.Empty, sqry = string.Empty, sqry1 = string.Empty;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();


        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                f_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                t_date.Text = DateTime.Now.ToString("dd/MM/yyyy");

                level = Session["level"].ToString();
                userid = Session["New"].ToString();
               
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
            assgn_roles();
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('146','1052','1085','147','64','65','22','1034','148','25','21','14','15','1086')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;
            ps_lbl2.Text = txtinfo.ToTitleCase("Syer Anggota");

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }


    }

    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0141' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();
                if (role_add == "1")
                {
                    Button2.Visible = true;
                }
                else
                {
                    Button2.Visible = false;
                }

            }
        }
    }



   

    //void wilahBind()
    //{
    //    DataSet Ds = new DataSet();
    //    try
    //    {
    //        string com = "select wilayah_name,wilayah_code From Ref_Cawangan group by wilayah_name,wilayah_code order by wilayah_name asc ";
    //        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
    //        DataTable dt = new DataTable();
    //        adpt.Fill(dt);
    //        DD_wilayah.DataSource = dt;
    //        DD_wilayah.DataTextField = "wilayah_name";
    //        DD_wilayah.DataValueField = "wilayah_code";
    //        DD_wilayah.DataBind();
    //        DD_wilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}



    protected void rst_clk(object sender, EventArgs e)
    {
        Response.Redirect("../keanggotan/LK_SENARI.aspx");
    }

    void get_details()
    {
        string fdate = f_date.Text;
        DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        String fmdate = fd.ToString("yyyy-MM-dd");

        string tdate = t_date.Text;
        DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        String tmdate = td.ToString("yyyy-MM-dd");


        if (DD_STS_ANGGO.SelectedValue == "01")
        {
            sqry = "select row_number() OVER (order by Applicant_Name,wilayah_name,cawangan_name) as Id,mem_name,mem_member_no,mem_new_icno,cast(wilayah_name as char(15)) wilayah_name,cast(cawangan_name as char(15)) cawangan_name,sha_item,FORMAT(sha_txn_dt,'dd/MM/yyyy', 'en-us') sha_txn_dt,sha_debit_amt,Applicant_Name from (select mem_name,mem_member_no,mem_new_icno,b.wilayah_name,b.cawangan_name,c.Applicant_Name from mem_member m left join Ref_Cawangan b on b.cawangan_code=m.mem_branch_cd and b.wilayah_code=m.mem_region_cd  left join Ref_Applicant_Category C on C.Applicant_Code=m.mem_applicant_type_cd where Acc_sts='Y') a inner join (select sha_new_icno,sha_txn_dt,sha_item,sha_debit_amt from mem_share ms1 where ms1.Acc_sts='Y' and sha_item='TAMBAHAN SYER' and sha_approve_sts_cd='SA' and  (sha_approve_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_approve_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1))) b on b.sha_new_icno=a.mem_new_icno order by Applicant_Name,wilayah_name,cawangan_name";
        }
        else if (DD_STS_ANGGO.SelectedValue == "04")
        {
            sqry = "select row_number() OVER (order by Applicant_Name,wilayah_name,cawangan_name) as Id,mem_name,mem_member_no,mem_new_icno,cast(wilayah_name as char(15)) wilayah_name,cast(cawangan_name as char(15)) cawangan_name,sha_item,FORMAT(sha_txn_dt,'dd/MM/yyyy', 'en-us') sha_txn_dt,sha_debit_amt,Applicant_Name from (select mem_name,mem_member_no,mem_new_icno,b.wilayah_name,b.cawangan_name,c.Applicant_Name from mem_member m left join Ref_Cawangan b on b.cawangan_code=m.mem_branch_cd and b.wilayah_code=m.mem_region_cd  left join Ref_Applicant_Category C on C.Applicant_Code=m.mem_applicant_type_cd where Acc_sts='Y') a inner join (select sha_new_icno,sha_txn_dt,sha_item,sha_debit_amt from mem_share ms1 where ms1.Acc_sts='Y' and sha_item='DIPINDAHKAN SEBAGAI SYER ANGGOTA' and sha_approve_sts_cd='SA' and  (sha_approve_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_approve_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1))) b on b.sha_new_icno=a.mem_new_icno order by Applicant_Name,wilayah_name,cawangan_name";
        }
        else if (DD_STS_ANGGO.SelectedValue == "02")
        {
            sqry = "select row_number() OVER (order by Applicant_Name,wilayah_name,cawangan_name) as Id,mem_name,mem_member_no,mem_new_icno,cast(wilayah_name as char(15)) wilayah_name,cast(cawangan_name as char(15)) cawangan_name,set_pay_method_cd AS sha_item,FORMAT(set_appprove_dt,'dd/MM/yyyy', 'en-us') sha_txn_dt ,set_apply_amt AS sha_debit_amt,Applicant_Name from (select mem_name,mem_member_no,mem_new_icno,b.wilayah_name,b.cawangan_name,c.Applicant_Name from mem_member m left join Ref_Cawangan b on b.cawangan_code=m.mem_branch_cd and b.wilayah_code=m.mem_region_cd  left join Ref_Applicant_Category C on C.Applicant_Code=m.mem_applicant_type_cd where Acc_sts='Y') a inner join (select set_new_icno, set_appl_type_cd,ms2.Payment_Type as  set_pay_method_cd,set_appprove_dt,set_apply_amt from mem_settlement ms1 left join Ref_Kaedah_Pembayaran ms2 on ms2.Payment_Code=ms1.set_pay_method_cd where ms1.Acc_sts='Y' and set_approve_sts_cd='SA' and set_pay_method_cd='TES' and  (set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1))) b on b.set_new_icno=a.mem_new_icno order by Applicant_Name,wilayah_name,cawangan_name";
        }
        else if (DD_STS_ANGGO.SelectedValue == "03")
        {
            sqry = "select row_number() OVER (order by Applicant_Name,wilayah_name,cawangan_name) as Id,mem_name,mem_member_no,mem_new_icno,cast(wilayah_name as char(15)) wilayah_name,cast(cawangan_name as char(15)) cawangan_name,set_pay_method_cd AS sha_item,FORMAT(set_appprove_dt,'dd/MM/yyyy', 'en-us') sha_txn_dt ,set_apply_amt AS sha_debit_amt,Applicant_Name from (select mem_name,mem_member_no,mem_new_icno,b.wilayah_name,b.cawangan_name,c.Applicant_Name from mem_member m left join Ref_Cawangan b on b.cawangan_code=m.mem_branch_cd and b.wilayah_code=m.mem_region_cd  left join Ref_Applicant_Category C on C.Applicant_Code=m.mem_applicant_type_cd where Acc_sts='Y') a inner join (select set_new_icno, set_appl_type_cd,ms2.Payment_Type as  set_pay_method_cd,set_appprove_dt,set_apply_amt from mem_settlement ms1 left join Ref_Kaedah_Pembayaran ms2 on ms2.Payment_Code=ms1.set_pay_method_cd where ms1.Acc_sts='Y' and set_approve_sts_cd='SA' and set_pay_method_cd='PIS' and  (set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1))) b on b.set_new_icno=a.mem_new_icno order by Applicant_Name,wilayah_name,cawangan_name";
        }
        else if (DD_STS_ANGGO.SelectedValue == "05")
        {
            sqry = "select row_number() OVER (order by Applicant_Name,wilayah_name,cawangan_name) as Id,mem_name,mem_member_no,mem_new_icno,cast(wilayah_name as char(15)) wilayah_name,cast(cawangan_name as char(15)) cawangan_name,set_pay_method_cd AS sha_item,FORMAT(set_appprove_dt,'dd/MM/yyyy', 'en-us') sha_txn_dt ,set_apply_amt AS sha_debit_amt,Applicant_Name from (select mem_name,mem_member_no,mem_new_icno,b.wilayah_name,b.cawangan_name,c.Applicant_Name from mem_member m left join Ref_Cawangan b on b.cawangan_code=m.mem_branch_cd and b.wilayah_code=m.mem_region_cd  left join Ref_Applicant_Category C on C.Applicant_Code=m.mem_applicant_type_cd where Acc_sts='Y') a inner join (select set_new_icno, set_appl_type_cd,ms2.Payment_Type as  set_pay_method_cd,set_appprove_dt,set_apply_amt from mem_settlement ms1 left join Ref_Kaedah_Pembayaran ms2 on ms2.Payment_Code=ms1.set_pay_method_cd where ms1.Acc_sts='Y' and set_approve_sts_cd='SA' and set_pay_method_cd='PUS' and  (set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1))) b on b.set_new_icno=a.mem_new_icno order by Applicant_Name,wilayah_name,cawangan_name";
        }
        else if (DD_STS_ANGGO.SelectedValue == "06")
        {
            sqry = "select row_number() OVER (order by Applicant_Name,wilayah_name,cawangan_name) as Id,mem_name,mem_member_no,mem_new_icno,cast(wilayah_name as char(15)) wilayah_name,cast(cawangan_name as char(15)) cawangan_name,set_pay_method_cd AS sha_item,FORMAT(set_appprove_dt,'dd/MM/yyyy', 'en-us') sha_txn_dt ,set_apply_amt AS sha_debit_amt,Applicant_Name from (select mem_name,mem_member_no,mem_new_icno,b.wilayah_name,b.cawangan_name,c.Applicant_Name from mem_member m left join Ref_Cawangan b on b.cawangan_code=m.mem_branch_cd and b.wilayah_code=m.mem_region_cd  left join Ref_Applicant_Category C on C.Applicant_Code=m.mem_applicant_type_cd where Acc_sts='Y') a inner join (select set_new_icno, set_appl_type_cd,ms2.Payment_Type as  set_pay_method_cd,set_appprove_dt,set_apply_amt from mem_settlement ms1 left join Ref_Kaedah_Pembayaran ms2 on ms2.Payment_Code=ms1.set_pay_method_cd where ms1.Acc_sts='Y' and set_approve_sts_cd='SA' and set_pay_method_cd='DTS' and  (set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1))) b on b.set_new_icno=a.mem_new_icno order by Applicant_Name,wilayah_name,cawangan_name";
        }

    }

    void get_details1()
    {
        string fdate = f_date.Text;
        DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        String fmdate = fd.ToString("yyyy-MM-dd");

        string tdate = t_date.Text;
        DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        String tmdate = td.ToString("yyyy-MM-dd");


        if (DD_STS_ANGGO.SelectedValue == "01" )
        {
            sqry = "select row_number() OVER (ORDER BY F.Applicant_Name,wilayah_name,cawangan_name,sha_item) as Id,Applicant_Name,cast(wilayah_name as char(15)) wilayah_name,cast(cawangan_name as char(15)) cawangan_name,count(mem_new_icno) Bil_ang,isnull(sha_item,'') as SI,SUM(tot_syer)  Jumlah from (select Applicant_Name,wilayah_name,cawangan_name,mem_new_icno,sha_item,tot_syer from (select mem_new_icno,b.wilayah_name,b.cawangan_name,c.Applicant_Name from mem_member m left join Ref_Cawangan b on b.cawangan_code=m.mem_branch_cd and b.wilayah_code=m.mem_region_cd  left join Ref_Applicant_Category C on C.Applicant_Code=m.mem_applicant_type_cd where Acc_sts='Y' ) a inner join (select sha_new_icno,sha_item,sha_debit_amt tot_syer from mem_share ms1 where ms1.Acc_sts='Y' and sha_item='TAMBAHAN SYER' and sha_approve_sts_cd='SA' and  (sha_approve_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_approve_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) ) b on b.sha_new_icno=a.mem_new_icno) F group by F.Applicant_Name,wilayah_name,cawangan_name,sha_item";
        }
        else if (DD_STS_ANGGO.SelectedValue == "04" )
        {
            sqry = "select row_number() OVER (ORDER BY F.Applicant_Name,wilayah_name,cawangan_name,sha_item) as Id,Applicant_Name,cast(wilayah_name as char(15)) wilayah_name,cast(cawangan_name as char(15)) cawangan_name,count(mem_new_icno) Bil_ang,isnull(sha_item,'') as SI,SUM(tot_syer)  Jumlah  from (select Applicant_Name,wilayah_name,cawangan_name,mem_new_icno,sha_item,tot_syer from (select mem_new_icno,b.wilayah_name,b.cawangan_name,c.Applicant_Name from mem_member m left join Ref_Cawangan b on b.cawangan_code=m.mem_branch_cd and b.wilayah_code=m.mem_region_cd  left join Ref_Applicant_Category C on C.Applicant_Code=m.mem_applicant_type_cd where Acc_sts='Y' ) a inner join (select sha_new_icno,sha_item,sha_debit_amt tot_syer from mem_share ms1 where ms1.Acc_sts='Y' and sha_item='DIPINDAHKAN SEBAGAI SYER ANGGOTA' and sha_approve_sts_cd='SA' and  (sha_approve_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_approve_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) ) b on b.sha_new_icno=a.mem_new_icno) F group by F.Applicant_Name,wilayah_name,cawangan_name,sha_item";
        }
        else if (DD_STS_ANGGO.SelectedValue == "02")
        {
            sqry = "select row_number() OVER (ORDER BY F.Applicant_Name,wilayah_name,cawangan_name,set_pay_method_cd) as Id,Applicant_Name,cast(wilayah_name as char(15)) wilayah_name,cast(cawangan_name as char(15)) cawangan_name,count(mem_new_icno) Bil_ang,isnull(set_pay_method_cd,'') as SI,SUM(tot_set)  Jumlah from (select Applicant_Name,wilayah_name,cawangan_name,mem_new_icno,set_pay_method_cd,tot_set from (select mem_new_icno,b.wilayah_name,b.cawangan_name,c.Applicant_Name from mem_member m left join Ref_Cawangan b on b.cawangan_code=m.mem_branch_cd and b.wilayah_code=m.mem_region_cd  left join Ref_Applicant_Category C on C.Applicant_Code=m.mem_applicant_type_cd where Acc_sts='Y' ) a inner join (select set_new_icno,ms2.Payment_Type as set_pay_method_cd, set_apply_amt as tot_set  from mem_settlement ms1 left join Ref_Kaedah_Pembayaran ms2 on ms2.Payment_Code=ms1.set_pay_method_cd where ms1.Acc_sts='Y' and set_approve_sts_cd='SA' and set_pay_method_cd='TES' and  (set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) ) b on b.set_new_icno=a.mem_new_icno) F group by F.Applicant_Name,wilayah_name,cawangan_name,set_pay_method_cd";
        }
        else if (DD_STS_ANGGO.SelectedValue == "03")
        {
            sqry = "select row_number() OVER (ORDER BY F.Applicant_Name,wilayah_name,cawangan_name,set_pay_method_cd) as Id,Applicant_Name,cast(wilayah_name as char(15)) wilayah_name,cast(cawangan_name as char(15)) cawangan_name,count(mem_new_icno) Bil_ang,isnull(set_pay_method_cd,'') as SI,SUM(tot_set)  Jumlah from (select Applicant_Name,wilayah_name,cawangan_name,mem_new_icno,set_pay_method_cd,tot_set from (select mem_new_icno,b.wilayah_name,b.cawangan_name,c.Applicant_Name from mem_member m left join Ref_Cawangan b on b.cawangan_code=m.mem_branch_cd and b.wilayah_code=m.mem_region_cd  left join Ref_Applicant_Category C on C.Applicant_Code=m.mem_applicant_type_cd where Acc_sts='Y' ) a inner join (select set_new_icno,ms2.Payment_Type as set_pay_method_cd, set_apply_amt as tot_set  from mem_settlement ms1 left join Ref_Kaedah_Pembayaran ms2 on ms2.Payment_Code=ms1.set_pay_method_cd where ms1.Acc_sts='Y' and set_approve_sts_cd='SA' and set_pay_method_cd='PIS' and  (set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) ) b on b.set_new_icno=a.mem_new_icno) F group by F.Applicant_Name,wilayah_name,cawangan_name,set_pay_method_cd";
        }
        else if (DD_STS_ANGGO.SelectedValue == "05")
        {
            sqry = "select row_number() OVER (ORDER BY F.Applicant_Name,wilayah_name,cawangan_name,set_pay_method_cd) as Id,Applicant_Name,cast(wilayah_name as char(15)) wilayah_name,cast(cawangan_name as char(15)) cawangan_name,count(mem_new_icno) Bil_ang,isnull(set_pay_method_cd,'') as SI,SUM(tot_set)  Jumlah from (select Applicant_Name,wilayah_name,cawangan_name,mem_new_icno,set_pay_method_cd,tot_set from (select mem_new_icno,b.wilayah_name,b.cawangan_name,c.Applicant_Name from mem_member m left join Ref_Cawangan b on b.cawangan_code=m.mem_branch_cd and b.wilayah_code=m.mem_region_cd  left join Ref_Applicant_Category C on C.Applicant_Code=m.mem_applicant_type_cd where Acc_sts='Y' ) a inner join (select set_new_icno,ms2.Payment_Type as set_pay_method_cd, set_apply_amt as tot_set  from mem_settlement ms1 left join Ref_Kaedah_Pembayaran ms2 on ms2.Payment_Code=ms1.set_pay_method_cd where  ms1.Acc_sts='Y' and set_approve_sts_cd='SA' and set_pay_method_cd='PUS' and  (set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) ) b on b.set_new_icno=a.mem_new_icno) F group by F.Applicant_Name,wilayah_name,cawangan_name,set_pay_method_cd";
        }
        else if (DD_STS_ANGGO.SelectedValue == "06")
        {
            sqry = "select row_number() OVER (ORDER BY F.Applicant_Name,wilayah_name,cawangan_name,set_pay_method_cd) as Id,Applicant_Name,cast(wilayah_name as char(15)) wilayah_name,cast(cawangan_name as char(15)) cawangan_name,count(mem_new_icno) Bil_ang,isnull(set_pay_method_cd,'') as SI,SUM(tot_set)  Jumlah from (select Applicant_Name,wilayah_name,cawangan_name,mem_new_icno,set_pay_method_cd,tot_set from (select mem_new_icno,b.wilayah_name,b.cawangan_name,c.Applicant_Name from mem_member m left join Ref_Cawangan b on b.cawangan_code=m.mem_branch_cd and b.wilayah_code=m.mem_region_cd  left join Ref_Applicant_Category C on C.Applicant_Code=m.mem_applicant_type_cd where Acc_sts='Y' ) a inner join (select set_new_icno,ms2.Payment_Type as set_pay_method_cd, set_apply_amt as tot_set  from mem_settlement ms1 left join Ref_Kaedah_Pembayaran ms2 on ms2.Payment_Code=ms1.set_pay_method_cd where ms1.Acc_sts='Y' and set_approve_sts_cd='SA' and set_pay_method_cd='DTS' and  (set_appprove_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and set_appprove_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)) ) b on b.set_new_icno=a.mem_new_icno) F group by F.Applicant_Name,wilayah_name,cawangan_name,set_pay_method_cd";
        }


    }

    void grid()
    {
        get_details();
        string hd_nm1 = string.Empty;
        if (DD_STS_ANGGO.SelectedValue == "01")
        {
            hd_nm1 = "TAMBAH SYER";
        }
        else if (DD_STS_ANGGO.SelectedValue == "02")
        {
            hd_nm1 = "PENEBUSAN SYER";
        }
        else if (DD_STS_ANGGO.SelectedValue == "03")
        {
            hd_nm1 = "PEMINDAHAN SYER";
        }
        else
        {
            hd_nm1 = "KREDIT KE AKAUN SYER";
        }
        dt = DBCon.Ora_Execute_table(sqry);
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        //Building an HTML string.
        StringBuilder htmlTable = new StringBuilder();
        htmlTable.Append("<table border='1' id='gvSelected' cell style='width:100%;' class='table uppercase table-striped'>");
        htmlTable.Append("<tr style='background-color:#BDC4C7;'> <th>Nama Anggota</th> <th> No Anggota </th><th> No. IC Anggota </th><th> Wilayah </th><th> Cawangan </th><th> Jenis Syer </th><th> Tarikh Sah Syer </th><th> Syer (RM) </th><th> Kategori Anggota </th> </tr>");

        if (!object.Equals(ds.Tables[0], null))
        {
            //Building the Data rows.
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    htmlTable.Append("<tr class='text-uppercase'>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["mem_name"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["mem_member_no"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["mem_new_icno"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["wilayah_name"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["cawangan_name"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["sha_item"] + "</td>");
                    htmlTable.Append("<td align='center'>" + ds.Tables[0].Rows[i]["sha_txn_dt"] + "</td>");
                    htmlTable.Append("<td align='Right'>" + double.Parse(ds.Tables[0].Rows[i]["sha_debit_amt"].ToString()).ToString("C").Replace("RM", "").Replace("$", "") + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["Applicant_Name"] + "</td>");
                    htmlTable.Append("</tr>");
                }
                Button1.Visible = true;
                Button3.Visible = true;
                htmlTable.Append("</table>");
                PlaceHolder1.Controls.Add(new Literal { Text = htmlTable.ToString() });
            }
            else
            {
                Button1.Visible = false;
                Button3.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }

        }
        string script1 = "$(function () { $('#gvSelected').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); $('.select2').select2() });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    void grid1()
    {
        get_details1();
        dt = DBCon.Ora_Execute_table(sqry);
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);
        //Building an HTML string.
        StringBuilder htmlTable = new StringBuilder();
        htmlTable.Append("<table border='1' id='GridView1' cell style='width:100%;' class='table uppercase table-striped'>");
        htmlTable.Append("<tr style='background-color:#BDC4C7;'> <th>KATEGORI ANGGOTA</th><th> NAMA WILLAYA </th><th> NAMA CAWANGAN </th><th> BIL. ANGGOTA </th><th> Jenis Syer </th><th> JUMLAH SYER (RM) </th></tr>");

        if (!object.Equals(ds.Tables[0], null))
        {
            //Building the Data rows.
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    htmlTable.Append("<tr class='text-uppercase'>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["Applicant_Name"] + "</td>");                    
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["wilayah_name"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["cawangan_name"] + "</td>");
                    htmlTable.Append("<td align='center'>" + ds.Tables[0].Rows[i]["Bil_ang"] + "</td>");
                    htmlTable.Append("<td>" + ds.Tables[0].Rows[i]["SI"] + "</td>");                    
                    htmlTable.Append("<td align='Right'>" + double.Parse(ds.Tables[0].Rows[i]["Jumlah"].ToString()).ToString("C").Replace("RM", "").Replace("$", "") + "</td>");
                    htmlTable.Append("</tr>");
                }
                Button1.Visible = true;
                Button3.Visible = true;
                htmlTable.Append("</table>");
                PlaceHolder2.Controls.Add(new Literal { Text = htmlTable.ToString() });
            }
            else
            {
                Button1.Visible = false;
                Button3.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

            }

        }
        string script1 = "$(function () { $('#GridView1').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); $('.select2').select2() });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void clk_batal(object sender, EventArgs e)
    {
        Response.Redirect("../keanggotan/Lp_anggota_koop.aspx");
    }
    protected void ExportToPDF(object sender, EventArgs e)
    {
        try
        {
            if (f_date.Text != "" && t_date.Text != "")
            {
                if (DD_STS_ANGGO.SelectedValue != "")
                {

                    if (DropDownList1.SelectedValue == "01")
                    {
                        get_details1();
                        rdlc_name = "keanggotan/LP_anggota_syer_ring.rdlc";
                        DS_name = "ang_syer_ring";
                    }

                    else
                    {
                        get_details();
                        rdlc_name = "keanggotan/Lp_anggota_syer_sen.rdlc";
                        DS_name = "DataSet1";
                    }

                    string hd_nm1 = string.Empty, hd_nm2 = string.Empty;
                    if (DD_STS_ANGGO.SelectedValue == "01")
                    {
                        hd_nm1 = "TAMBAH SYER";
                        hd_nm2 = "NewSyer";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "02")
                    {
                        hd_nm1 = "PENEBUSAN SYER";
                        hd_nm2 = "Penebusan";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "03")
                    {
                        hd_nm1 = "PEMINDAHAN SYER";
                        hd_nm2 = "Pemindahan";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "05")
                    {
                        hd_nm1 = "PEMULANGAN SYER";
                        hd_nm2 = "Pemulangan";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "06")
                    {
                        hd_nm1 = "DANA TEBUS SYER";
                        hd_nm2 = "Dana_Tebus";
                    }
                    else
                    {
                        hd_nm1 = "KREDIT KE AKAUN SYER";
                        hd_nm2 = "CRSyer";
                    }

                    dt = DBCon.Ora_Execute_table(sqry);
                    ds.Tables.Add(dt);
                    ReportViewer1.Reset();
                    ReportViewer1.LocalReport.Refresh();
                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    if (countRow != 0)
                    {
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.ReportPath = rdlc_name;
                        ReportDataSource rds = new ReportDataSource(DS_name, dt);
                        ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", f_date.Text),
                     new ReportParameter("s2", t_date.Text),
                     new ReportParameter("s3", DD_STS_ANGGO.SelectedItem.Text),
                     new ReportParameter("s4", hd_nm1)
                        
                          };

                        ReportViewer1.LocalReport.SetParameters(rptParams);
                        ReportViewer1.LocalReport.DataSources.Add(rds);
                        //Refresh
                        ReportViewer1.LocalReport.Refresh();
                        filename = string.Format("{0}.{1}", "IMS_" + hd_nm2 + "_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                        //}
                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string extension;

                        byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();
                        System.Threading.Thread.Sleep(1);

                        //List<ReportParameter> paramList = new List<ReportParameter>();
                        //paramList.Add(new ReportParameter("RowsPerPage", "30"));
                        //RptviwerLKSENARI.LocalReport.SetParameters(paramList);


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Status Anggota.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);


                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Response.Redirect("LK_SENARI.aspx");
        }
    }

    protected void ExportToEXCEL(object sender, EventArgs e)
    {
        try
        {
            if (f_date.Text != "" && t_date.Text != "")
            {
                if (DD_STS_ANGGO.SelectedValue != "")
                {
                    if (DropDownList1.SelectedValue == "01")
                    {
                        get_details1();
                    }
                    else
                    {
                        get_details();
                    }
                    dt = DBCon.Ora_Execute_table(sqry);
                    ds.Tables.Add(dt);
                    ReportViewer1.Reset();
                    ReportViewer1.LocalReport.Refresh();
                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();
                    string hd_nm1 = string.Empty, hd_nm2 = string.Empty;
                    if (DD_STS_ANGGO.SelectedValue == "01")
                    {
                        hd_nm1 = "TAMBAH SYER";
                        hd_nm2 = "NewSyer";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "02")
                    {
                        hd_nm1 = "PENEBUSAN SYER";
                        hd_nm2 = "Penebusan";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "03")
                    {
                        hd_nm1 = "PEMINDAHAN SYER";
                        hd_nm2 = "Pemindahan";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "05")
                    {
                        hd_nm1 = "PEMULANGAN SYER";
                        hd_nm2 = "Pemulangan";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "06")
                    {
                        hd_nm1 = "DANA TEBUS SYER";
                        hd_nm2 = "Dana_Tebus";
                    }
                    else
                    {
                        hd_nm1 = "KREDIT KE AKAUN SYER";
                        hd_nm2 = "CRSyer";
                    }
                    if (countRow != 0)
                    {

                        StringBuilder builder = new StringBuilder();
                        string strFileName = string.Format("{0}.{1}", "IMS_" + hd_nm2 + "_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                        if (DropDownList1.SelectedValue == "02")
                        {

                            builder.Append("Nama Anggota,No Anggota,No. IC Anggota,Wilayah,Cawangan,Jenis Syer,Tarikh Sah Syer,Syer (RM), Kategori Anggota" + Environment.NewLine);
                            for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                            {

                                builder.Append(dt.Rows[k]["mem_name"].ToString() + " , " + dt.Rows[k]["mem_member_no"].ToString() + ", " + dt.Rows[k]["mem_new_icno"].ToString() + "," + dt.Rows[k]["wilayah_name"].ToString() + "," + dt.Rows[k]["cawangan_name"].ToString() + "," + dt.Rows[k]["sha_item"].ToString() + "," + dt.Rows[k]["sha_txn_dt"].ToString() + "," + dt.Rows[k]["sha_debit_amt"].ToString() + "," + dt.Rows[k]["Applicant_Name"].ToString() + Environment.NewLine);

                            }
                           
                        }
                        else
                        {
                            builder.Append("Kategori Anggota,Wilayah,Cawangan,Bil.Anggota,Jenis Syer, Jumlah Syer (RM)" + Environment.NewLine);
                            for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                            {

                                builder.Append(dt.Rows[k]["Applicant_Name"].ToString() + " , " + dt.Rows[k]["wilayah_name"].ToString() + ", " + dt.Rows[k]["cawangan_name"].ToString() + "," + dt.Rows[k]["Bil_ang"].ToString() + "," + dt.Rows[k]["SI"].ToString() + "," + dt.Rows[k]["Jumlah"].ToString()  + Environment.NewLine);

                            }
                        }
                        Response.Clear();
                        Response.ContentType = "text/csv";
                        Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                        Response.Write(builder.ToString());
                        Response.End();


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Status Anggota.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);


                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
            //Response.Redirect("LK_SENARI.aspx");
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        try
        {

            if (DD_STS_ANGGO.SelectedValue != "" && DropDownList1.SelectedValue != "")
            {
                if (DropDownList1.SelectedValue == "02")
                {                  
                    grid();

                }
                else
                {
                    grid1();
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                string script1 = "$(function () {$('.select2').select2()  });";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
            }
        }

        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();
        }
    }
}