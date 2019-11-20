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


public partial class LK_SENARI : System.Web.UI.Page
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
                kawasan();
                //wilahBind();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('146','1052','1085','147','64','65','22','1034','148','25','21','14','15','1086')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;
            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
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
    protected void DD_kaw_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {
            string com = "select distinct wilayah_code,wilayah_name from Ref_Cawangan where kavasan_name='" + DD_kaw.SelectedItem.Text + "' order by wilayah_name asc ";
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
        Response.Redirect("../keanggotan/LK_SENARI.aspx");
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

                    if (DD_kaw.SelectedValue == "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                    {
                        //dt = DBCon.Ora_Execute_table("select mem_new_icno,ac.Applicant_Name,mem_sahabat_no,mem_name,mem_centre,case when ISNULL(a.t_amount,'') = '' then '0.00' else a.t_amount end as fee_amount,mem_address,mem_postcd,ISNULL(rn.Decription,'') Decription,rg.gender_desc,FORMAT(mfee.fee_approval_dt,'dd/MM/yyyy', 'en-us') as fee_approval_dt,ISNULL(set2.DESCRRIPTION,'') as sebab,FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') as reg_dt,mm.mem_remark as remark,mfee.fee_amount as fiamt from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as t_amount From mem_share where Acc_sts ='Y' group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join mem_fee as mfee on mfee.fee_new_icno=mm.mem_new_icno and mfee.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd  where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and MM.mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and MM.mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.t_amount,ac.Applicant_Name,MM.mem_postcd,rn.Decription,rg.gender_desc,mfee.fee_approval_dt,set2.DESCRRIPTION,mm.mem_register_dt,mm.mem_remark,mfee.fee_amount order by mem_centre,cawangan_name,mem_new_icno,mem_sahabat_no");
                        dt = DBCon.Ora_Execute_table("select d.mem_new_icno,d.Applicant_Name,a.mem_member_no as mem_sahabat_no,d.mem_name,d.mem_centre,sum(d.tot_syer) as fee_amount,d.mem_address,d.mem_postcd,ISNULL(rn.Decription,'') Decription,rg.gender_desc,FORMAT(d.fee_approval_dt,'dd/MM/yyyy', 'en-us') as fee_approval_dt,ISNULL(d.sebab,'') as sebab,FORMAT(d.mem_register_dt,'dd/MM/yyyy', 'en-us') as reg_dt,d.mem_remark as remark,d.fee_sum as fiamt from (select * from mem_member where mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and Acc_sts='Y') as a left join (select sum(ISNULL(ast_st_balance_amt,'0.00')) ast_st_balance_amt,ast_new_icno from aim_st where ast_start_date>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and ast_end_date<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by ast_new_icno) as b on b.ast_new_icno = a.mem_new_icno left join (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH inner join mem_member as mm on SH.sha_new_icno=MM.mem_new_icno and mm.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno= mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd where  MM.Acc_sts ='Y' and  mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as c on c.sha_new_icno=a.mem_new_icno left join (select mem_new_icno,mem_name,ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,mem_sahabat_no,mem_postcd,mem_address,mem_register_dt,mem_remark,mem_negri,mem_gender_cd,ISNULL((SUM(SH.sha_debit_amt)- SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,fee_approval_dt,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y'  left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE= set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code, wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd where MM.Acc_sts ='Y' and mem_sts_cd='SA'   group by mem_branch_cd, mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION,mem_new_icno,mem_name,mem_sahabat_no,mem_postcd,mem_address,mem_negri,mem_gender_cd,mem_register_dt,mem_remark,fee_approval_dt) as d on d.mem_new_icno=a.mem_new_icno left join Ref_Negeri rn on rn.Decription_Code=d.mem_negri Left join ref_gender rg on rg.gender_cd=d.mem_gender_cd group by d.mem_new_icno,d.Applicant_Name,a.mem_member_no,d.mem_name,d.mem_centre,d.wilayah_name,d.cawangan_name,d.sebab,d.mem_address,d.mem_postcd,rn.Decription,rg.gender_desc,d.fee_approval_dt,d.mem_register_dt,d.mem_remark,d.fee_sum");
                        ds.Tables.Add(dt);

                        //dt = ds.Tables[0];
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                    {
                        //dt = DBCon.Ora_Execute_table("select mem_new_icno,ac.Applicant_Name,mem_sahabat_no,mem_name,mem_centre,case when ISNULL(a.t_amount,'') = '' then '0.00' else a.t_amount end as fee_amount,mem_address,mem_postcd,ISNULL(rn.Decription,'') Decription,rg.gender_desc,FORMAT(mfee.fee_approval_dt,'dd/MM/yyyy', 'en-us') as fee_approval_dt,ISNULL(set2.DESCRRIPTION,'') as sebab,FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') as reg_dt,mm.mem_remark as remark,mfee.fee_amount as fiamt from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as t_amount From mem_share where Acc_sts ='Y' group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join mem_fee as mfee on mfee.fee_new_icno=mm.mem_new_icno and mfee.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y'  left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd  where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and MM.mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and MM.mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd='" + skav_no + "' group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.t_amount,ac.Applicant_Name,MM.mem_postcd,rn.Decription,rg.gender_desc,mfee.fee_approval_dt,set2.DESCRRIPTION,mm.mem_register_dt,mm.mem_remark,mfee.fee_amount order by mem_centre,cawangan_name,mem_new_icno,mem_sahabat_no");
                        dt = DBCon.Ora_Execute_table("select d.mem_new_icno,d.Applicant_Name,d.mem_sahabat_no,d.mem_name,d.mem_centre,sum(d.tot_syer) as fee_amount,d.mem_address,d.mem_postcd,ISNULL(rn.Decription,'') Decription,rg.gender_desc,FORMAT(d.fee_approval_dt,'dd/MM/yyyy', 'en-us') as fee_approval_dt,ISNULL(d.sebab,'') as sebab,FORMAT(d.mem_register_dt,'dd/MM/yyyy', 'en-us') as reg_dt,d.mem_remark as remark,d.fee_sum as fiamt from (select * from mem_member where mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and mem_area_cd='" + skav_no + "' and Acc_sts='Y') as a left join (select sum(ISNULL(ast_st_balance_amt,'0.00')) ast_st_balance_amt,ast_new_icno from aim_st where ast_start_date>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and ast_end_date<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by ast_new_icno) as b on b.ast_new_icno = a.mem_new_icno left join (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH inner join mem_member as mm on SH.sha_new_icno=MM.mem_new_icno and mm.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno= mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd where  MM.Acc_sts ='Y' and  mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and mem_area_cd='" + skav_no + "' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as c on c.sha_new_icno=a.mem_new_icno left join (select mem_new_icno,mem_name,ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,mem_sahabat_no,mem_postcd,mem_address,mem_register_dt,mem_remark,mem_negri,mem_gender_cd,ISNULL((SUM(SH.sha_debit_amt)- SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,fee_approval_dt,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y'  left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE= set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code, wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd where MM.Acc_sts ='Y' and mem_sts_cd='SA'   group by mem_branch_cd, mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION,mem_new_icno,mem_name,mem_sahabat_no,mem_postcd,mem_address,mem_negri,mem_gender_cd,mem_register_dt,mem_remark,fee_approval_dt) as d on d.mem_new_icno=a.mem_new_icno left join Ref_Negeri rn on rn.Decription_Code=d.mem_negri Left join ref_gender rg on rg.gender_cd=d.mem_gender_cd group by d.mem_new_icno,d.Applicant_Name,d.mem_sahabat_no,d.mem_name,d.mem_centre,d.wilayah_name,d.cawangan_name,d.sebab,d.mem_address,d.mem_postcd,rn.Decription,rg.gender_desc,d.fee_approval_dt,d.mem_register_dt,d.mem_remark,d.fee_sum");
                        ds.Tables.Add(dt);
                        //dt = ds.Tables[0];
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                    {
                        //dt = DBCon.Ora_Execute_table("select mem_new_icno,ac.Applicant_Name,mem_sahabat_no,mem_name,mem_centre,case when ISNULL(a.t_amount,'') = '' then '0.00' else a.t_amount end as fee_amount,mem_address,mem_postcd,ISNULL(rn.Decription,'') Decription,rg.gender_desc,FORMAT(mfee.fee_approval_dt,'dd/MM/yyyy', 'en-us') as fee_approval_dt,ISNULL(set2.DESCRRIPTION,'') as sebab,FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') as reg_dt,mm.mem_remark as remark,mfee.fee_amount as fiamt from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as t_amount From mem_share where Acc_sts ='Y' group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join mem_fee as mfee on mfee.fee_new_icno=mm.mem_new_icno and mfee.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd  where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and MM.mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and MM.mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd='" + skav_no + "'  and mem_region_cd='" + DD_wilayah.SelectedValue + "' group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.t_amount,ac.Applicant_Name,MM.mem_postcd,rn.Decription,rg.gender_desc,mfee.fee_approval_dt,set2.DESCRRIPTION,mm.mem_register_dt,mm.mem_remark,mfee.fee_amount order by mem_centre,cawangan_name,mem_new_icno,mem_sahabat_no");
                        dt = DBCon.Ora_Execute_table("select d.mem_new_icno,d.Applicant_Name,d.mem_sahabat_no,d.mem_name,d.mem_centre,sum(d.tot_syer) as fee_amount,d.mem_address,d.mem_postcd,ISNULL(rn.Decription,'') Decription,rg.gender_desc,FORMAT(d.fee_approval_dt,'dd/MM/yyyy', 'en-us') as fee_approval_dt,ISNULL(d.sebab,'') as sebab,FORMAT(d.mem_register_dt,'dd/MM/yyyy', 'en-us') as reg_dt,d.mem_remark as remark,d.fee_sum as fiamt from (select * from mem_member where mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and mem_region_cd='" + DD_wilayah.SelectedValue + "' and Acc_sts='Y') as a left join (select sum(ISNULL(ast_st_balance_amt,'0.00')) ast_st_balance_amt,ast_new_icno from aim_st where ast_start_date>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and ast_end_date<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by ast_new_icno) as b on b.ast_new_icno = a.mem_new_icno left join (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH inner join mem_member as mm on SH.sha_new_icno=MM.mem_new_icno and mm.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno= mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd where  MM.Acc_sts ='Y' and  mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and mem_region_cd='" + DD_wilayah.SelectedValue + "' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as c on c.sha_new_icno=a.mem_new_icno left join (select mem_new_icno,mem_name,ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,mem_sahabat_no,mem_postcd,mem_address,mem_register_dt,mem_remark,mem_negri,mem_gender_cd,ISNULL((SUM(SH.sha_debit_amt)- SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,fee_approval_dt,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y'  left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE= set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code, wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd where MM.Acc_sts ='Y' and mem_sts_cd='SA'   group by mem_branch_cd, mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION,mem_new_icno,mem_name,mem_sahabat_no,mem_postcd,mem_address,mem_negri,mem_gender_cd,mem_register_dt,mem_remark,fee_approval_dt) as d on d.mem_new_icno=a.mem_new_icno left join Ref_Negeri rn on rn.Decription_Code=d.mem_negri Left join ref_gender rg on rg.gender_cd=d.mem_gender_cd group by d.mem_new_icno,d.Applicant_Name,d.mem_sahabat_no,d.mem_name,d.mem_centre,d.wilayah_name,d.cawangan_name,d.sebab,d.mem_address,d.mem_postcd,rn.Decription,rg.gender_desc,d.fee_approval_dt,d.mem_register_dt,d.mem_remark,d.fee_sum");
                        ds.Tables.Add(dt);
                        //dt = ds.Tables[0];
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue != "" && DD_STS_ANGGO.SelectedValue != "")
                    {
                        //dt = DBCon.Ora_Execute_table("select mem_new_icno,ac.Applicant_Name,mem_sahabat_no,mem_name,mem_centre,case when ISNULL(a.t_amount,'') = '' then '0.00' else a.t_amount end as fee_amount,mem_address,mem_postcd,ISNULL(rn.Decription,'') Decription,rg.gender_desc,FORMAT(mfee.fee_approval_dt,'dd/MM/yyyy', 'en-us') as fee_approval_dt,ISNULL(set2.DESCRRIPTION,'') as sebab,FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') as reg_dt,mm.mem_remark as remark,mfee.fee_amount as fiamt from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as t_amount From mem_share where Acc_sts ='Y' group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join mem_fee as mfee on mfee.fee_new_icno=mm.mem_new_icno and mfee.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd  where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and MM.mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and MM.mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)  and  mem_branch_cd='" + DD_cawangan.SelectedValue + "'  group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.t_amount,ac.Applicant_Name,MM.mem_postcd,rn.Decription,rg.gender_desc,mfee.fee_approval_dt,set2.DESCRRIPTION,mm.mem_register_dt,mm.mem_remark,mfee.fee_amount order by mem_centre,cawangan_name,mem_new_icno,mem_sahabat_no");
                        dt = DBCon.Ora_Execute_table("select d.mem_new_icno,d.Applicant_Name,d.mem_sahabat_no,d.mem_name,d.mem_centre,sum(d.tot_syer) as fee_amount,d.mem_address,d.mem_postcd,ISNULL(rn.Decription,'') Decription,rg.gender_desc,FORMAT(d.fee_approval_dt,'dd/MM/yyyy', 'en-us') as fee_approval_dt,ISNULL(d.sebab,'') as sebab,FORMAT(d.mem_register_dt,'dd/MM/yyyy', 'en-us') as reg_dt,d.mem_remark as remark,d.fee_sum as fiamt from (select * from mem_member where mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and  mem_branch_cd='" + DD_cawangan.SelectedValue + "' and Acc_sts='Y') as a left join (select sum(ISNULL(ast_st_balance_amt,'0.00')) ast_st_balance_amt,ast_new_icno from aim_st where ast_start_date>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and ast_end_date<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by ast_new_icno) as b on b.ast_new_icno = a.mem_new_icno left join (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH inner join mem_member as mm on SH.sha_new_icno=MM.mem_new_icno and mm.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno= mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd where  MM.Acc_sts ='Y' and  mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and  mem_branch_cd='" + DD_cawangan.SelectedValue + "' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as c on c.sha_new_icno=a.mem_new_icno left join (select mem_new_icno,mem_name,ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,mem_sahabat_no,mem_postcd,mem_address,mem_register_dt,mem_remark,mem_negri,mem_gender_cd,ISNULL((SUM(SH.sha_debit_amt)- SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,fee_approval_dt,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y'  left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE= set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code, wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd where MM.Acc_sts ='Y' and mem_sts_cd='SA'   group by mem_branch_cd, mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION,mem_new_icno,mem_name,mem_sahabat_no,mem_postcd,mem_address,mem_negri,mem_gender_cd,mem_register_dt,mem_remark,fee_approval_dt) as d on d.mem_new_icno=a.mem_new_icno left join Ref_Negeri rn on rn.Decription_Code=d.mem_negri Left join ref_gender rg on rg.gender_cd=d.mem_gender_cd group by d.mem_new_icno,d.Applicant_Name,d.mem_sahabat_no,d.mem_name,d.mem_centre,d.wilayah_name,d.cawangan_name,d.sebab,d.mem_address,d.mem_postcd,rn.Decription,rg.gender_desc,d.fee_approval_dt,d.mem_register_dt,d.mem_remark,d.fee_sum");
                        ds.Tables.Add(dt);
                        //dt = ds.Tables[0];
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue != "" && DD_STS_ANGGO.SelectedValue != "")
                    {
                        //dt = DBCon.Ora_Execute_table("select mem_new_icno,ac.Applicant_Name,mem_sahabat_no,mem_name,mem_centre,case when ISNULL(a.t_amount,'') = '' then '0.00' else a.t_amount end as fee_amount,mem_address,mem_postcd,ISNULL(rn.Decription,'') Decription,rg.gender_desc,FORMAT(mfee.fee_approval_dt,'dd/MM/yyyy', 'en-us') as fee_approval_dt,ISNULL(set2.DESCRRIPTION,'') as sebab,FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') as reg_dt,mm.mem_remark as remark,mfee.fee_amount as fiamt from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as t_amount From mem_share where Acc_sts ='Y' group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join mem_fee as mfee on mfee.fee_new_icno=mm.mem_new_icno and mfee.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd  where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and MM.mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and MM.mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)  mem_branch_cd='" + DD_cawangan.SelectedValue + "' and mem_centre like '%" + txt_pusat.Text + "%'  group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.t_amount,ac.Applicant_Name,MM.mem_postcd,rn.Decription,rg.gender_desc,mfee.fee_approval_dt,set2.DESCRRIPTION,mm.mem_register_dt,mm.mem_remark,mfee.fee_amount order by mem_centre,cawangan_name,mem_new_icno,mem_sahabat_no");
                        dt = DBCon.Ora_Execute_table("select d.mem_new_icno,d.Applicant_Name,d.mem_sahabat_no,d.mem_name,d.mem_centre,sum(d.tot_syer) as fee_amount,d.mem_address,d.mem_postcd,ISNULL(rn.Decription,'') Decription,rg.gender_desc,FORMAT(d.fee_approval_dt,'dd/MM/yyyy', 'en-us') as fee_approval_dt,ISNULL(d.sebab,'') as sebab,FORMAT(d.mem_register_dt,'dd/MM/yyyy', 'en-us') as reg_dt,d.mem_remark as remark,d.fee_sum as fiamt from (select * from mem_member where mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and Acc_sts='Y' and  mem_branch_cd='" + DD_cawangan.SelectedValue + "' and mem_centre like '%" + txt_pusat.Text + "%') as a left join (select sum(ISNULL(ast_st_balance_amt,'0.00')) ast_st_balance_amt,ast_new_icno from aim_st where ast_start_date>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and ast_end_date<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by ast_new_icno) as b on b.ast_new_icno = a.mem_new_icno left join (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH inner join mem_member as mm on SH.sha_new_icno=MM.mem_new_icno and mm.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno= mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd where  MM.Acc_sts ='Y' and  mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and  mem_branch_cd='" + DD_cawangan.SelectedValue + "' and mem_centre like '%" + txt_pusat.Text + "%' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as c on c.sha_new_icno=a.mem_new_icno left join (select mem_new_icno,mem_name,ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,mem_sahabat_no,mem_postcd,mem_address,mem_register_dt,mem_remark,mem_negri,mem_gender_cd,ISNULL((SUM(SH.sha_debit_amt)- SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,fee_approval_dt,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y'  left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE= set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code, wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd where MM.Acc_sts ='Y' and mem_sts_cd='SA'   group by mem_branch_cd, mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION,mem_new_icno,mem_name,mem_sahabat_no,mem_postcd,mem_address,mem_negri,mem_gender_cd,mem_register_dt,mem_remark,fee_approval_dt) as d on d.mem_new_icno=a.mem_new_icno left join Ref_Negeri rn on rn.Decription_Code=d.mem_negri Left join ref_gender rg on rg.gender_cd=d.mem_gender_cd group by d.mem_new_icno,d.Applicant_Name,d.mem_sahabat_no,d.mem_name,d.mem_centre,d.wilayah_name,d.cawangan_name,d.sebab,d.mem_address,d.mem_postcd,rn.Decription,rg.gender_desc,d.fee_approval_dt,d.mem_register_dt,d.mem_remark,d.fee_sum");
                        ds.Tables.Add(dt);
                        //dt = ds.Tables[0];
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
                   
                    else if (DD_kaw.SelectedValue == "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue == "" && DD_STS_ANGGO.SelectedValue != "")
                    {
                        //dt = DBCon.Ora_Execute_table("select mem_new_icno,ac.Applicant_Name,mem_sahabat_no,mem_name,mem_centre,case when ISNULL(a.t_amount,'') = '' then '0.00' else a.t_amount end as fee_amount,mem_address,mem_postcd,ISNULL(rn.Decription,'') Decription,rg.gender_desc,FORMAT(mfee.fee_approval_dt,'dd/MM/yyyy', 'en-us') as fee_approval_dt,ISNULL(set2.DESCRRIPTION,'') as sebab,FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') as reg_dt,mm.mem_remark as remark,mfee.fee_amount as fiamt from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as t_amount From mem_share where Acc_sts ='Y' group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join mem_fee as mfee on mfee.fee_new_icno=mm.mem_new_icno and mfee.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd  where MM.Acc_sts ='Y' and mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and MM.mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and MM.mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_centre like '%" + txt_pusat.Text + "%' group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.t_amount,ac.Applicant_Name,MM.mem_postcd,rn.Decription,rg.gender_desc,mfee.fee_approval_dt,set2.DESCRRIPTION,mm.mem_register_dt,mm.mem_remark,mfee.fee_amount order by mem_centre,cawangan_name,mem_new_icno,mem_sahabat_no");
                        dt = DBCon.Ora_Execute_table("select d.mem_new_icno,d.Applicant_Name,d.mem_sahabat_no,d.mem_name,d.mem_centre,sum(d.tot_syer) as fee_amount,d.mem_address,d.mem_postcd,ISNULL(rn.Decription,'') Decription,rg.gender_desc,FORMAT(d.fee_approval_dt,'dd/MM/yyyy', 'en-us') as fee_approval_dt,ISNULL(d.sebab,'') as sebab,FORMAT(d.mem_register_dt,'dd/MM/yyyy', 'en-us') as reg_dt,d.mem_remark as remark,d.fee_sum as fiamt from (select * from mem_member where mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and Acc_sts='Y' and mem_centre like '%" + txt_pusat.Text + "%') as a left join (select sum(ISNULL(ast_st_balance_amt,'0.00')) ast_st_balance_amt,ast_new_icno from aim_st where ast_start_date>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and ast_end_date<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by ast_new_icno) as b on b.ast_new_icno = a.mem_new_icno left join (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH inner join mem_member as mm on SH.sha_new_icno=MM.mem_new_icno and mm.Acc_sts ='Y' left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno= mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE=set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd where  MM.Acc_sts ='Y' and  mem_sts_cd='" + DD_STS_ANGGO.SelectedValue + "' and mem_centre like '%" + txt_pusat.Text + "%' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as c on c.sha_new_icno=a.mem_new_icno left join (select mem_new_icno,mem_name,ac.Applicant_Name,mem_centre,wilayah_name,cawangan_name,mem_sahabat_no,mem_postcd,mem_address,mem_register_dt,mem_remark,mem_negri,mem_gender_cd,ISNULL((SUM(SH.sha_debit_amt)- SUM(SH.sha_credit_amt)),'0.00') as tot_syer,ISNULL(sum(fee_amount),'0.00') fee_sum,fee_approval_dt,ISNULL(set2.DESCRRIPTION,'') as sebab from mem_member as MM left join mem_share as SH on SH.sha_new_icno=MM.mem_new_icno and SH.Acc_sts ='Y'  left join mem_fee as FE on FE.fee_new_icno=MM.mem_new_icno and FE.Acc_sts ='Y' left join mem_settlement set1 on set1.set_new_icno=mm.mem_new_icno and set1.Acc_sts ='Y' left join Ref_Sebab set2 on set2.DESCRRIPTION_CODE= set1.set_reason_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=mem_applicant_type_cd full outer join (select cawangan_code,wilayah_code,cawangan_name,wilayah_name From Ref_Cawangan group by cawangan_code, wilayah_code,cawangan_name,wilayah_name ) as CA on CA.cawangan_code=MM.mem_branch_cd and CA.wilayah_code=MM.mem_region_cd where MM.Acc_sts ='Y' and mem_sts_cd='SA'   group by mem_branch_cd, mem_region_cd,cawangan_name,wilayah_name,ac.Applicant_Name,mem_centre,set2.DESCRRIPTION,mem_new_icno,mem_name,mem_sahabat_no,mem_postcd,mem_address,mem_negri,mem_gender_cd,mem_register_dt,mem_remark,fee_approval_dt) as d on d.mem_new_icno=a.mem_new_icno left join Ref_Negeri rn on rn.Decription_Code=d.mem_negri Left join ref_gender rg on rg.gender_cd=d.mem_gender_cd group by d.mem_new_icno,d.Applicant_Name,d.mem_sahabat_no,d.mem_name,d.mem_centre,d.wilayah_name,d.cawangan_name,d.sebab,d.mem_address,d.mem_postcd,rn.Decription,rg.gender_desc,d.fee_approval_dt,d.mem_register_dt,d.mem_remark,d.fee_sum");
                        ds.Tables.Add(dt);


                        //dt = ds.Tables[0];
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
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Rekod Tidak Dijumpai.');", true);//
                    }

                    string ss1 = string.Empty, ss2 = string.Empty, ss3 = string.Empty, ss4 = string.Empty, ss5 = string.Empty, rdlc_name = string.Empty, DS_name = string.Empty;

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
                        rdlc_name = "keanggotan/LK_SENARI_FM.rdlc";
                        DS_name = "LK_SENARI_FM";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "SA")
                    {
                        ss5 = "SAH";
                        rdlc_name = "keanggotan/LK_SENARI.rdlc";
                        DS_name = "LK_SENARI";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "TS")
                    {
                        ss5 = "TIDAK SAH";
                        rdlc_name = "keanggotan/LK_SENARI_TS.rdlc";
                        DS_name = "LK_SENARI_TS";
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "DN")
                    {
                        ss5 = "DALAM NOTIS";
                        rdlc_name = "keanggotan/LK_SENARI.rdlc";
                        DS_name = "LK_SENARI";
                    }

                    RptviwerLKSENARI.Reset();
                    RptviwerLKSENARI.LocalReport.Refresh();
                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();
                  
                    if (countRow != 0)
                    {
                        ss1_stap1.Visible = true;
                        RptviwerLKSENARI.LocalReport.DataSources.Clear();
                        RptviwerLKSENARI.LocalReport.ReportPath = rdlc_name;
                        ReportDataSource rds = new ReportDataSource(DS_name, dt);
                        ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", f_date.Text),
                     new ReportParameter("s2", t_date.Text),
                     new ReportParameter("s3", ss1),
                     new ReportParameter("s4", ss2),
                     new ReportParameter("s5", ss3),
                     new ReportParameter("s6", ss4),
                     new ReportParameter("s7", ss5),
                     new ReportParameter("s8", DD_STS_ANGGO.SelectedValue)
                          };

                        RptviwerLKSENARI.LocalReport.SetParameters(rptParams);
                        RptviwerLKSENARI.LocalReport.DataSources.Add(rds);
                        //Refresh
                        RptviwerLKSENARI.LocalReport.Refresh();
                        System.Threading.Thread.Sleep(1);

                        //List<ReportParameter> paramList = new List<ReportParameter>();
                        //paramList.Add(new ReportParameter("RowsPerPage", "30"));
                        //RptviwerLKSENARI.LocalReport.SetParameters(paramList);


                    }
                    else
                    {
                        ss1_stap1.Visible = false;
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
}