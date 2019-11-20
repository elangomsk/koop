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

public partial class DN_SENARI : System.Web.UI.Page
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
                //sts_anggota();
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('1089','1052','1090','147','64','65','22','1034','148','25','1091','14','1042','1089','1092')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[12][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl11.Text = txtinfo.ToTitleCase(gt_lng.Rows[13][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl14.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0143' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

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
            string com = "select cawangan_code,cawangan_name From Ref_Cawangan where  kavasan_name='" + DD_kaw.SelectedValue + "' and wilayah_name='" + DD_wilayah.SelectedItem.Text + "' order by cawangan_name asc";
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
        Response.Redirect("../keanggotan/DN_SENARI.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (f_date.Text != "" && t_date.Text != "")
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

                if (DD_STS_ANGGO.SelectedValue == "01")
                {
                    if (DD_kaw.SelectedValue == "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee,FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') as draft_dt from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and sha_item ='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno  left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,mm.mem_register_dt order by mem_centre,cawangan_name,mem_new_icno,mem_sahabat_no");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee,FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') as draft_dt from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and sha_item ='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno  and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,mm.mem_register_dt order by mem_centre,cawangan_name,mem_new_icno,mem_sahabat_no");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee,FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') as draft_dt from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and sha_item ='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' and  mem_region_cd='" + DD_wilayah.SelectedValue + "'  group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,mm.mem_register_dt order by mem_centre,cawangan_name,mem_new_icno,mem_sahabat_no");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue != "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee,FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') as draft_dt from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and sha_item ='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' and  mem_region_cd='" + DD_wilayah.SelectedValue + "'  and  mem_branch_cd='" + DD_cawangan.SelectedValue + "'  group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,mm.mem_register_dt order by mem_centre,cawangan_name,mem_new_icno,mem_sahabat_no");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue != "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee,FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') as draft_dt from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and sha_item ='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' and  mem_region_cd='" + DD_wilayah.SelectedValue + "'  and  mem_branch_cd='" + DD_cawangan.SelectedValue + "' and mem_centre like '%" + txt_pusat.Text + "%'  group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,mm.mem_register_dt order by mem_centre,cawangan_name,mem_new_icno,mem_sahabat_no");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee,FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') as draft_dt from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and sha_item ='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' and mem_centre like '%" + txt_pusat.Text + "%' group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,mm.mem_register_dt order by mem_centre,cawangan_name,mem_new_icno,mem_sahabat_no");
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
                    else if (DD_kaw.SelectedValue == "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee,FORMAT(mm.mem_register_dt,'dd/MM/yyyy', 'en-us') as draft_dt from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and  sha_item ='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_centre like '%" + txt_pusat.Text + "%' group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,mm.mem_register_dt order by mem_centre,cawangan_name,mem_new_icno,mem_sahabat_no");
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
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);//
                    }
                }
                else if (DD_STS_ANGGO.SelectedValue == "02")
                {
                    if (DD_kaw.SelectedValue == "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,ISNULL(a.samt,'0.00') as tot_amt,ISNULL(rs.DESCRRIPTION,'') as sebab,FORMAT(a.set_txn_dt,'dd/MM/yyyy', 'en-us') as mohon_dt from mem_member as MM full outer join (select set_new_icno,sum(set_apply_amt) as samt,set_reason_cd,set_txn_dt From mem_settlement where Acc_sts ='Y' group by set_new_icno,set_reason_cd,set_txn_dt) as a on a.set_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join Ref_Sebab rs on rs.DESCRRIPTION_CODE=a.set_reason_cd  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.samt,rs.DESCRRIPTION,a.set_txn_dt order by wilayah_name,cawangan_name,mem_centre");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,ISNULL(a.samt,'0.00') as tot_amt,ISNULL(rs.DESCRRIPTION,'') as sebab,FORMAT(a.set_txn_dt,'dd/MM/yyyy', 'en-us') as mohon_dt from mem_member as MM full outer join (select set_new_icno,sum(set_apply_amt) as samt,set_reason_cd,set_txn_dt From mem_settlement where Acc_sts ='Y' group by set_new_icno,set_reason_cd,set_txn_dt) as a on a.set_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join Ref_Sebab rs on rs.DESCRRIPTION_CODE=a.set_reason_cd where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.samt,rs.DESCRRIPTION,a.set_txn_dt order by wilayah_name,cawangan_name,mem_centre");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,ISNULL(a.samt,'0.00') as tot_amt,ISNULL(rs.DESCRRIPTION,'') as sebab,FORMAT(a.set_txn_dt,'dd/MM/yyyy', 'en-us') as mohon_dt from mem_member as MM full outer join (select set_new_icno,sum(set_apply_amt) as samt,set_reason_cd,set_txn_dt From mem_settlement where Acc_sts ='Y' group by set_new_icno,set_reason_cd,set_txn_dt) as a on a.set_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join Ref_Sebab rs on rs.DESCRRIPTION_CODE=a.set_reason_cd where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' and  mem_region_cd='" + DD_wilayah.SelectedValue + "' mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.samt,rs.DESCRRIPTION,a.set_txn_dt order by wilayah_name,cawangan_name,mem_centre");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue != "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,ISNULL(a.samt,'0.00') as tot_amt,ISNULL(rs.DESCRRIPTION,'') as sebab,FORMAT(a.set_txn_dt,'dd/MM/yyyy', 'en-us') as mohon_dt from mem_member as MM full outer join (select set_new_icno,sum(set_apply_amt) as samt,set_reason_cd,set_txn_dt From mem_settlement where Acc_sts ='Y' group by set_new_icno,set_reason_cd,set_txn_dt) as a on a.set_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join Ref_Sebab rs on rs.DESCRRIPTION_CODE=a.set_reason_cd where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' and  mem_region_cd='" + DD_wilayah.SelectedValue + "'  and  mem_branch_cd='" + DD_cawangan.SelectedValue + "'  group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.samt,rs.DESCRRIPTION,a.set_txn_dt order by wilayah_name,cawangan_name,mem_centre");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue != "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,ISNULL(a.samt,'0.00') as tot_amt,ISNULL(rs.DESCRRIPTION,'') as sebab,FORMAT(a.set_txn_dt,'dd/MM/yyyy', 'en-us') as mohon_dt from mem_member as MM full outer join (select set_new_icno,sum(set_apply_amt) as samt,set_reason_cd,set_txn_dt From mem_settlement where Acc_sts ='Y' group by set_new_icno,set_reason_cd,set_txn_dt) as a on a.set_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join Ref_Sebab rs on rs.DESCRRIPTION_CODE=a.set_reason_cd where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' and  mem_region_cd='" + DD_wilayah.SelectedValue + "'  and  mem_branch_cd='" + DD_cawangan.SelectedValue + "' and mem_centre like '%" + txt_pusat.Text + "%'  group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.samt,rs.DESCRRIPTION,a.set_txn_dt order by wilayah_name,cawangan_name,mem_centre");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,ISNULL(a.samt,'0.00') as tot_amt,ISNULL(rs.DESCRRIPTION,'') as sebab,FORMAT(a.set_txn_dt,'dd/MM/yyyy', 'en-us') as mohon_dt from mem_member as MM full outer join (select set_new_icno,sum(set_apply_amt) as samt,set_reason_cd,set_txn_dt From mem_settlement where Acc_sts ='Y' group by set_new_icno,set_reason_cd,set_txn_dt) as a on a.set_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join Ref_Sebab rs on rs.DESCRRIPTION_CODE=a.set_reason_cd where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' and mem_centre like '%" + txt_pusat.Text + "%' group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.samt,rs.DESCRRIPTION,a.set_txn_dt order by wilayah_name,cawangan_name,mem_centre");
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
                    else if (DD_kaw.SelectedValue == "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_new_icno,mem_sahabat_no,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,ISNULL(a.samt,'0.00') as tot_amt,ISNULL(rs.DESCRRIPTION,'') as sebab,FORMAT(a.set_txn_dt,'dd/MM/yyyy', 'en-us') as mohon_dt from mem_member as MM full outer join (select set_new_icno,sum(set_apply_amt) as samt,set_reason_cd,set_txn_dt From mem_settlement where Acc_sts ='Y'  group by set_new_icno,set_reason_cd,set_txn_dt) as a on a.set_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join Ref_Sebab rs on rs.DESCRRIPTION_CODE=a.set_reason_cd where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_centre like '%" + txt_pusat.Text + "%' group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.samt,rs.DESCRRIPTION,a.set_txn_dt order by wilayah_name,cawangan_name,mem_centre");
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
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);//
                    }
                }
                else if (DD_STS_ANGGO.SelectedValue == "03")
                {
                    if (DD_kaw.SelectedValue == "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_sahabat_no,mem_new_icno,ac.Applicant_Name,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno  left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,ac.Applicant_Name order by wilayah_name,cawangan_name,mem_centre");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_sahabat_no,mem_new_icno,ac.Applicant_Name,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno  left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,ac.Applicant_Name order by wilayah_name,cawangan_name,mem_centre");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_sahabat_no,mem_new_icno,ac.Applicant_Name,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno  left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' and  mem_region_cd='" + DD_wilayah.SelectedValue + "'  group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,ac.Applicant_Name order by wilayah_name,cawangan_name,mem_centre");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text == "" && DD_cawangan.SelectedValue != "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_sahabat_no,mem_new_icno,ac.Applicant_Name,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno  left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' and  mem_region_cd='" + DD_wilayah.SelectedValue + "'  and  mem_branch_cd='" + DD_cawangan.SelectedValue + "'  group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,ac.Applicant_Name order by wilayah_name,cawangan_name,mem_centre");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue != "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue != "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_sahabat_no,mem_new_icno,ac.Applicant_Name,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno  left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' and  mem_region_cd='" + DD_wilayah.SelectedValue + "'  and  mem_branch_cd='" + DD_cawangan.SelectedValue + "' and mem_centre like '%" + txt_pusat.Text + "%'  group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,ac.Applicant_Name order by wilayah_name,cawangan_name,mem_centre");
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
                    else if (DD_kaw.SelectedValue != "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_sahabat_no,mem_new_icno,ac.Applicant_Name,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno  left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_area_cd ='" + skav_no + "' and mem_centre like '%" + txt_pusat.Text + "%' group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,ac.Applicant_Name order by wilayah_name,cawangan_name,mem_centre");
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
                    else if (DD_kaw.SelectedValue == "" && DD_wilayah.SelectedValue == "" && txt_pusat.Text != "" && DD_cawangan.SelectedValue == "")
                    {
                        dt = DBCon.Ora_Execute_table("select mem_sahabat_no,mem_new_icno,ac.Applicant_Name,wl.wilayah_name,wl.cawangan_name,mem_centre,mem_name,mem_address,case when ISNULL(a.fee_amount,'') = '' then '0.00' else a.fee_amount end as tot_syer,sum(mf.fee_amount) as tot_fee from mem_member as MM full outer join (select sha_new_icno,(SUM(sha_debit_amt)-SUM(sha_credit_amt)) as fee_amount From mem_share where Acc_sts ='Y' and sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) group by sha_new_icno) as a on a.sha_new_icno=MM.mem_new_icno left join aim_st as AST on AST.ast_new_icno=MM.mem_new_icno left join mem_share as FEE on FEE.sha_new_icno=MM.mem_new_icno and FEE.Acc_sts ='Y' left join Ref_Cawangan as WL on WL.Wilayah_Code=MM.mem_region_cd and WL.cawangan_code=MM.mem_branch_cd left join Ref_Applicant_Category ac on ac.Applicant_Code=MM.mem_applicant_type_cd left join Ref_Negeri rn on rn.Decription_Code=MM.mem_negri Left join ref_gender rg on rg.gender_cd=MM.mem_gender_cd left join mem_fee mf on mf.fee_new_icno=MM.mem_new_icno and mf.Acc_sts ='Y'  where MM.Acc_sts ='Y' and mem_sts_cd IN ('DN','') and fee.sha_item !='ANGGOTA BAHARU' and sha_txn_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and sha_txn_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) and mem_centre like '%" + txt_pusat.Text + "%' group by mem_new_icno,mem_sahabat_no,wilayah_name,cawangan_name,mem_name,mem_address,mem_centre,a.fee_amount,ac.Applicant_Name order by wilayah_name,cawangan_name,mem_centre");
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
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);//
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);//
                }

                string ss1 = string.Empty, ss2 = string.Empty, ss3 = string.Empty, ss4 = string.Empty, ss5 = string.Empty;

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
                }
                else if (DD_STS_ANGGO.SelectedValue == "SA")
                {
                    ss5 = "ANGGOTA SAH";
                }
                else if (DD_STS_ANGGO.SelectedValue == "TS")
                {
                    ss5 = "ANGGOTA TIDAK SAH";
                }
                else if (DD_STS_ANGGO.SelectedValue == "DN")
                {
                    ss5 = "DALAM NOTIS";
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
                    string disname = string.Empty;
                    if (DD_STS_ANGGO.SelectedValue == "01")
                    {
                        RptviwerLKSENARI.LocalReport.ReportPath = "keanggotan/DN_SENARI1.rdlc";
                        ReportDataSource rds = new ReportDataSource("DN_SENARI1", dt);
                        RptviwerLKSENARI.LocalReport.DataSources.Add(rds);
                        disname = "ANGGOTA_BAHARU_" + DateTime.Now.ToString("ddMMyyyy");
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "02")
                    {
                        RptviwerLKSENARI.LocalReport.ReportPath = "keanggotan/DN_SENARI2.rdlc";
                        ReportDataSource rds = new ReportDataSource("DN_SENARI2", dt);
                        RptviwerLKSENARI.LocalReport.DataSources.Add(rds);
                        disname = "PENYELESAIAN_ANGGOTA_" + DateTime.Now.ToString("ddMMyyyy");
                    }
                    else if (DD_STS_ANGGO.SelectedValue == "03")
                    {
                        RptviwerLKSENARI.LocalReport.ReportPath = "keanggotan/DN_SENARI3.rdlc";
                        ReportDataSource rds = new ReportDataSource("DN_SENARI3", dt);
                        RptviwerLKSENARI.LocalReport.DataSources.Add(rds);
                        disname = "TAMBAHAN_SYER_" + DateTime.Now.ToString("ddMMyyyy");
                    }
                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("s1", f_date.Text),
                     new ReportParameter("s2", t_date.Text),
                     new ReportParameter("s3", ss1),
                     new ReportParameter("s4", ss2),
                     new ReportParameter("s5", ss3),
                     new ReportParameter("s6", ss4),
                     new ReportParameter("s7", ss5)
                          };

                    RptviwerLKSENARI.LocalReport.SetParameters(rptParams);
                    RptviwerLKSENARI.LocalReport.DisplayName = disname;
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
                ss1_stap1.Visible = false;
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