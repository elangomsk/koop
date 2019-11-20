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
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web.SessionState;
using System.Xml;
using Microsoft.Reporting.WinForms;
using Microsoft.Reporting.WebForms;

public partial class LK_RINGKASAN : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable wilayah = new DataTable();
    string level, userid;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.Button2);

        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                //wilahBind();
                kawasan();
                sts_anggota();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('149','1052','1087','150','64','65','22','1034','148','21','14','1042','1088')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;


            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl13.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());

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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0142' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

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





    void sts_anggota()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "SELECT Member_Description,Membership_Code FROM status_kelulusan where Membership_Code NOT IN ('DN') order by Member_Description asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_STS_ANGGO.DataSource = dt;
            DD_STS_ANGGO.DataTextField = "Member_Description";
            DD_STS_ANGGO.DataValueField = "Membership_Code";
            DD_STS_ANGGO.DataBind();
            DD_STS_ANGGO.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void kawasan()
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select DISTINCT Area_Code,Area_Name from Ref_Kawasan ORDER BY Area_Name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_kaw.DataSource = dt;
            DD_kaw.DataTextField = "Area_Name";
            DD_kaw.DataValueField = "Area_Name";
            DD_kaw.DataBind();
            DD_kaw.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
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

    protected void DD_kaw_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select distinct wilayah_code,wilayah_name from Ref_Cawangan where kavasan_name='" + DD_kaw.SelectedValue + "' order by wilayah_name asc ";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_wilayah.DataSource = dt;
            DD_wilayah.DataTextField = "wilayah_name";
            DD_wilayah.DataValueField = "wilayah_code";
            DD_wilayah.DataBind();
            DD_wilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void DD_wilayah_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select cawangan_code,cawangan_name From Ref_Cawangan where kavasan_name='" + DD_kaw.SelectedItem.Text + "' and wilayah_name='" + DD_wilayah.SelectedItem.Text + "' order by cawangan_name asc";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_cawangan.DataSource = dt;
            DD_cawangan.DataTextField = "cawangan_name";
            DD_cawangan.DataValueField = "cawangan_code";
            DD_cawangan.DataBind();
            DD_cawangan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rst_clk(object sender, EventArgs e)
    {
        Response.Redirect("../keanggotan/LK_RINGKASAN.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (f_date.Text != "" && t_date.Text != "")
            {
                if (DD_STS_ANGGO.SelectedValue != "")
                {
                    //Path
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();

                    string fdate = f_date.Text;
                    DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    String fmdate = fd.ToString("yyyy-MM-dd");

                    string tdate = t_date.Text;
                    DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    String tmdate = td.ToString("yyyy-MM-dd");

                    string skav_no = string.Empty;
                    if (DD_kaw.SelectedValue != "")
                    {
                        DataTable ddokdicno_pro = new DataTable();
                        ddokdicno_pro = DBCon.Ora_Execute_table("select DISTINCT Area_Code,Area_Name from Ref_Kawasan where Area_Name='" + DD_kaw.SelectedValue + "'");

                        skav_no = ddokdicno_pro.Rows[0]["Area_Code"].ToString();
                    }

                    if (DD_kaw.SelectedValue == "" && DD_wilayah.SelectedValue == "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                    {

                        dt = DBCon.Ora_Execute_table("select d.Applicant_Name,d.mem_centre,ISNULL(d.wilayah_name,'') wilayah_name,ISNULL(d.cawangan_name,'') cawangan_name,count(*) mem_sahabat_no,sum(d.tot_syer) as tot_syer,sum(d.fee_sum) as fee_sum,sum(ISNULL(b.ast_st_balance_amt,'0.00')) as ast_st_balance_amt,d.sebab from (select * from mem_member where mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "') as a left join (select sum(ISNULL(ast_st_balance_amt,'0.00')) ast_st_balance_amt,ast_new_icno from aim_st where ast_start_date>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and ast_end_date<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by ast_new_icno) as b on b.ast_new_icno = a.mem_new_icno left join (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH inner join mem_member as mm on SH.sha_new_icno=MM.mem_new_icno and mm.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno= mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd where  MM.Acc_sts ='Y' and  mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and SH.sha_txn_dt>=DATEADD (day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and SH.sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as c on c.sha_new_icno=a.mem_new_icno left join (select mem_new_icno,ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,count(*) as mem_sahabat_no,ISNULL((SUM(SH.sha_debit_amt)- SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y' and SH.sha_txn_dt>=DATEADD (day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and SH.sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)  left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE= set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code, wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "'   group by mem_branch_cd, mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION,mem_new_icno) as d on d.mem_new_icno=a.mem_new_icno group by d.Applicant_Name,d.mem_centre,d.wilayah_name,d.cawangan_name,d.sebab ");
                        //dt = DBCon.Ora_Execute_table("select d.Applicant_Name,d.mem_centre,ISNULL(d.wilayah_name,'') wilayah_name,ISNULL(d.cawangan_name,'') cawangan_name,count(*) mem_sahabat_no,sum(d.tot_syer) as tot_syer,sum(d.fee_sum) as fee_sum,sum(ISNULL(b.ast_st_balance_amt,'0.00')) as ast_st_balance_amt,d.sebab from (select ss2.mem_new_icno from mem_share ss1 inner join mem_member ss2 on ss2.mem_new_icno=ss1.sha_new_icno and ss2.Acc_sts ='Y' where mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and ss1.Acc_sts ='Y' and ss1.sha_txn_dt>=DATEADD (day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and ss1.sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)  group by mem_new_icno) as a left join (select sum(ISNULL(ast_st_balance_amt,'0.00')) ast_st_balance_amt,ast_new_icno from aim_st where ast_start_date>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and ast_end_date<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by ast_new_icno) as b on b.ast_new_icno = a.mem_new_icno left join (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH inner join mem_member as mm on SH.sha_new_icno=MM.mem_new_icno and mm.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno= mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd where  MM.Acc_sts ='Y' and  mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and SH.sha_txn_dt>=DATEADD (day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and SH.sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as c on c.sha_new_icno=a.mem_new_icno left join (select mem_new_icno,ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,count(*) as mem_sahabat_no,ISNULL((SUM(SH.sha_debit_amt)- SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y'  left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE= set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code, wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' group by mem_branch_cd, mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION,mem_new_icno) as d on d.mem_new_icno=a.mem_new_icno group by d.Applicant_Name,d.mem_centre,d.wilayah_name,d.cawangan_name,d.sebab order by d.Applicant_Name, d.mem_centre asc ");
                        ds.Tables.Add(dt);
                        if (dt.Rows.Count % 20 != 0)
                        {
                            int addCount = 20 - dt.Rows.Count % 20;
                            for (int i = 0; i < addCount; i++)
                            {
                                DataRow dr = dt.NewRow();
                                dr[0] = "";
                                dt.Rows.Add(dr);
                            }

                        }
                    }
                    //else if (DD_kaw.SelectedValue != "" &&  DD_wilayah.SelectedValue == "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                    //{
                    //    //dt = DBCon.Ora_Execute_table("select ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,count(*) mem_sahabat_no,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,ISNULL(SUM(ST.ast_st_balance_amt),'') as ast_st_balance_amt,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code,wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd left join aim_st as ST on ST.ast_new_icno=MM.mem_new_icno where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by mem_branch_cd,mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION order by wilayah_name,cawangan_name,mem_centre ");
                    //    dt = DBCon.Ora_Execute_table("select b.Applicant_Name,mem_centre,ISNULL(b.wilayah_name,'') wilayah_name,ISNULL(b.cawangan_name,'') cawangan_name,count(*) mem_sahabat_no,sum(b.tot_syer) as tot_syer,sum(b.fee_sum) as fee_sum,sum(b.ast_st_balance_amt) as ast_st_balance_amt,b.sebab from (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH left join mem_member as mm on SH.sha_new_icno=MM.mem_new_icno and mm.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd where  MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and  mem_branch_cd='" + DD_cawangan.SelectedValue + "' and mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a left join (select mem_new_icno,ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,count(*) as mem_sahabat_no,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,ISNULL(SUM(ST.ast_st_balance_amt),'') as ast_st_balance_amt,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code,wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd left join aim_st as ST on ST.ast_new_icno=MM.mem_new_icno where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and  mem_branch_cd='" + DD_cawangan.SelectedValue + "' and mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)  group by mem_branch_cd,mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION,mem_new_icno  ) as b on b.mem_new_icno=a.sha_new_icno group by b.Applicant_Name,mem_centre,b.wilayah_name,b.cawangan_name,b.sebab order by wilayah_name,cawangan_name,mem_centre ");
                    //    ds.Tables.Add(dt);
                    //        if (dt.Rows.Count % 20 != 0)
                    //        {
                    //            int addCount = 20 - dt.Rows.Count % 20;
                    //            for (int i = 0; i < addCount; i++)
                    //            {
                    //                DataRow dr = dt.NewRow();
                    //                dr[0] = "";
                    //                dt.Rows.Add(dr);
                    //            }

                    //        }
                    //    }
                    //else if (DD_kaw.SelectedValue != "" &&  DD_wilayah.SelectedValue != "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                    //{
                    //    //dt = DBCon.Ora_Execute_table("select ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,count(*) mem_sahabat_no,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,ISNULL(SUM(ST.ast_st_balance_amt),'') as ast_st_balance_amt,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code,wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd left join aim_st as ST on ST.ast_new_icno=MM.mem_new_icno where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by mem_branch_cd,mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION order by wilayah_name,cawangan_name,mem_centre ");
                    //    dt = DBCon.Ora_Execute_table("select b.Applicant_Name,mem_centre,ISNULL(b.wilayah_name,'') wilayah_name,ISNULL(b.cawangan_name,'') cawangan_name,count(*) mem_sahabat_no,sum(b.tot_syer) as tot_syer,sum(b.fee_sum) as fee_sum,sum(b.ast_st_balance_amt) as ast_st_balance_amt,b.sebab from (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH left join mem_member as mm on SH.sha_new_icno=MM.mem_new_icno and mm.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd where  MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and  mem_branch_cd='" + DD_cawangan.SelectedValue + "' and mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a left join (select mem_new_icno,ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,count(*) as mem_sahabat_no,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,ISNULL(SUM(ST.ast_st_balance_amt),'') as ast_st_balance_amt,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code,wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd left join aim_st as ST on ST.ast_new_icno=MM.mem_new_icno where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and  mem_branch_cd='" + DD_cawangan.SelectedValue + "' and mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)  group by mem_branch_cd,mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION,mem_new_icno  ) as b on b.mem_new_icno=a.sha_new_icno group by b.Applicant_Name,mem_centre,b.wilayah_name,b.cawangan_name,b.sebab order by wilayah_name,cawangan_name,mem_centre ");
                    //    ds.Tables.Add(dt);
                    //        if (dt.Rows.Count % 20 != 0)
                    //        {
                    //            int addCount = 20 - dt.Rows.Count % 20;
                    //            for (int i = 0; i < addCount; i++)
                    //            {
                    //                DataRow dr = dt.NewRow();
                    //                dr[0] = "";
                    //                dt.Rows.Add(dr);
                    //            }

                    //        }
                    //    }
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && DD_cawangan.SelectedValue != "" && DD_STS_ANGGO.SelectedValue != "")
                    {

                        dt = DBCon.Ora_Execute_table("select d.Applicant_Name,d.mem_centre,ISNULL(d.wilayah_name,'') wilayah_name,ISNULL(d.cawangan_name,'') cawangan_name,count(*) mem_sahabat_no,sum(d.tot_syer) as tot_syer,sum(d.fee_sum) as fee_sum,sum(ISNULL(b.ast_st_balance_amt,'0.00')) as ast_st_balance_amt,d.sebab from (select * from mem_member where mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and mem_branch_cd='" + DD_cawangan.SelectedValue + "') as a left join (select sum(ISNULL(ast_st_balance_amt,'0.00')) ast_st_balance_amt,ast_new_icno from aim_st where ast_start_date>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and ast_end_date<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by ast_new_icno) as b on b.ast_new_icno = a.mem_new_icno left join (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH inner join mem_member as mm on SH.sha_new_icno=MM.mem_new_icno and mm.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno= mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd where  MM.Acc_sts ='Y' and  mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and SH.sha_txn_dt>=DATEADD (day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and SH.sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as c on c.sha_new_icno=a.mem_new_icno left join (select mem_new_icno,ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,count(*) as mem_sahabat_no,ISNULL((SUM(SH.sha_debit_amt)- SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y' and SH.sha_txn_dt>=DATEADD (day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and SH.sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)  left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE= set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code, wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "'   group by mem_branch_cd, mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION,mem_new_icno) as d on d.mem_new_icno=a.mem_new_icno group by d.Applicant_Name,d.mem_centre,d.wilayah_name,d.cawangan_name,d.sebab ");
                        //dt = DBCon.Ora_Execute_table("select d.Applicant_Name,d.mem_centre,ISNULL(d.wilayah_name,'') wilayah_name,ISNULL(d.cawangan_name,'') cawangan_name,count(*) mem_sahabat_no,sum(d.tot_syer) as tot_syer,sum(d.fee_sum) as fee_sum,sum(ISNULL(b.ast_st_balance_amt,'0.00')) as ast_st_balance_amt,d.sebab from (select ss2.mem_new_icno from mem_share ss1 inner join mem_member ss2 on ss2.mem_new_icno=ss1.sha_new_icno and ss2.Acc_sts ='Y' where mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and ss1.Acc_sts ='Y' and ss1.sha_txn_dt>=DATEADD (day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and ss1.sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)  and ss2.mem_branch_cd='" + DD_cawangan.SelectedValue + "' group by mem_new_icno) as a left join (select sum(ISNULL(ast_st_balance_amt,'0.00')) ast_st_balance_amt,ast_new_icno from aim_st where ast_start_date>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and ast_end_date<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by ast_new_icno) as b on b.ast_new_icno = a.mem_new_icno left join (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH inner join mem_member as mm on SH.sha_new_icno=MM.mem_new_icno and mm.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno= mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd where  MM.Acc_sts ='Y' and  mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and SH.sha_txn_dt>=DATEADD (day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and SH.sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as c on c.sha_new_icno=a.mem_new_icno left join (select mem_new_icno,ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,count(*) as mem_sahabat_no,ISNULL((SUM(SH.sha_debit_amt)- SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y'  left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE= set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code, wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "'   group by mem_branch_cd, mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION,mem_new_icno) as d on d.mem_new_icno=a.mem_new_icno group by d.Applicant_Name,d.mem_centre,d.wilayah_name,d.cawangan_name,d.sebab d.Applicant_Name, d.mem_centre asc ");

                        ds.Tables.Add(dt);
                        if (dt.Rows.Count % 20 != 0)
                        {
                            int addCount = 20 - dt.Rows.Count % 20;
                            for (int i = 0; i < addCount; i++)
                            {
                                DataRow dr = dt.NewRow();
                                dr[0] = "";
                                dt.Rows.Add(dr);
                            }

                        }
                    }

                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Pilih Kawasan Alone.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);//


                    }
                    string ss1 = string.Empty, ss2 = string.Empty, ss3 = string.Empty, ss4 = string.Empty, ss5 = string.Empty, rdlc_name = string.Empty, ds_nme = string.Empty;

                    if (DD_kaw.SelectedValue != "")
                    {
                        ss1 = DD_kaw.SelectedItem.Text;
                    }
                    else
                    {
                        ss1 = "SEMUA";
                    }
                    if (DD_wilayah.SelectedValue != "")
                    {
                        ss2 = DD_wilayah.SelectedItem.Text;
                    }
                    else
                    {
                        ss2 = "SEMUA";
                    }
                    if (DD_cawangan.SelectedValue != "")
                    {
                        ss3 = DD_cawangan.SelectedItem.Text;
                    }
                    else
                    {
                        ss3 = "SEMUA";
                    }
                    if (DD_STS_ANGGO.SelectedValue != "")
                    {
                        ss4 = DD_STS_ANGGO.SelectedItem.Text;
                    }
                    else
                    {
                        ss4 = "SEMUA";
                    }
                    if (DD_STS_ANGGO.SelectedValue == "FM")
                    {
                        ss5 = "FI MASUK";
                        rdlc_name = "keanggotan/LK_RINGKASAN.rdlc";
                        ds_nme = "LK_RINGKASAN";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "SA")
                    {
                        ss5 = "ANGGOTA SAH";
                        rdlc_name = "keanggotan/LK_RINGKASAN.rdlc";
                        ds_nme = "LK_RINGKASAN";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "TS")
                    {
                        ss5 = "ANGGOTA TIDAK SAH";
                        rdlc_name = "keanggotan/LK_RINGKASAN_TS.rdlc";
                        ds_nme = "LK_RINGKASAN_TS";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "DN")
                    {
                        ss5 = "DALAM NOTIS";
                        rdlc_name = "keanggotan/LK_RINGKASAN.rdlc";
                        ds_nme = "LK_RINGKASAN";
                    }



                    RptviwerLKringkasan.Reset();
                    RptviwerLKringkasan.LocalReport.Refresh();
                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    if (countRow != 0)
                    {
                        ss1_stap1.Visible = true;
                        RptviwerLKringkasan.LocalReport.DataSources.Clear();
                        RptviwerLKringkasan.LocalReport.ReportPath = rdlc_name;
                        ReportDataSource rds = new ReportDataSource(ds_nme, dt);

                        ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", f_date.Text),
                     new ReportParameter("s2", t_date.Text),
                     new ReportParameter("s3", ss1),
                     new ReportParameter("s4", ss2),
                     new ReportParameter("s5", ss3),
                     new ReportParameter("s6", ss4),
                     new ReportParameter("s7", ss5)
                          };

                        RptviwerLKringkasan.LocalReport.SetParameters(rptParams);

                        RptviwerLKringkasan.LocalReport.DataSources.Add(rds);


                        //Refresh
                        RptviwerLKringkasan.LocalReport.Refresh();
                        System.Threading.Thread.Sleep(1);
                    }
                    else
                    {
                        ss1_stap1.Visible = false;
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);


                    }
                }
                else
                {
                    ss1_stap1.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Status Anggota.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);


                }
            }
            else
            {
                ss1_stap1.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }
        catch (Exception ex)
        {
            ss1_stap1.Visible = false;
            throw ex;
        }
    }
}