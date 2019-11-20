using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;
using System.Data;  

public partial class Dashboard : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    DataTable get_ahli_sa = new DataTable();
    DataTable get_jum_syer = new DataTable();
    DataTable get_sts_pa = new DataTable();
    DataTable get_sts_ts = new DataTable();
    DataTable jm_sts1 = new DataTable();
    DataTable jm_sts2 = new DataTable();
    DataTable jm_sts3 = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                view_details();
            }
            else
            {
                Response.Redirect("KSAIMB_Login.aspx");
            }
        }
        
    }
    
    void view_details()
    {
        if (Session["mnu_id"].ToString().Trim() == "M0007" || Session["mnu_id"].ToString().Trim() == "M0008")
        {
            module1.Visible = true;
            module2.Visible = false;
            module3.Visible = false;
            get_ahli_sa = DBCon.Ora_Execute_table("select count(a.mem_new_icno) as anggota,sum(ISNULL(d.fee_sum,'0.00')) as fiamt,sum(ISNULL(c.tot_syer,'0.00')) as syer from  (select * from mem_member where Acc_sts='Y' and (mem_sts_cd='SA')) as a left join (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH  where  SH.Acc_sts ='Y' and  sh.sha_approve_sts_cd='SA' group by sha_new_icno) as c on c.sha_new_icno=a.mem_new_icno  left join (select fee_new_icno,ISNULL(sum(fee_amount),'0.00') fee_sum from mem_fee fe where  fe.Acc_sts ='Y' and  fee_sts_cd='SA' group by fee_new_icno) as d on d.fee_new_icno=a.mem_new_icno ");
            get_sts_pa = DBCon.Ora_Execute_table("select count(*) as cnt from mem_member where mem_sts_cd In ('','DN') and Acc_sts='Y'");
            get_sts_ts = DBCon.Ora_Execute_table("select count(a.mem_new_icno) as anggota,sum(ISNULL(d.fee_sum,'0.00')) as fiamt,sum(ISNULL(c.tot_syer,'0.00')) as syer from  (select * from mem_member where Acc_sts='Y' and (mem_sts_cd='FM')) as a left join (select sha_new_icno,ISNULL((SUM(SH.sha_debit_amt)-SUM(SH.sha_credit_amt)),'0.00') as tot_syer from mem_share SH  where  SH.Acc_sts ='Y' and  sh.sha_approve_sts_cd='SA' group by sha_new_icno) as c on c.sha_new_icno=a.mem_new_icno  left join (select fee_new_icno,ISNULL(sum(fee_amount),'0.00') fee_sum from mem_fee fe where  fe.Acc_sts ='Y' and  fee_sts_cd='' group by fee_new_icno) as d on d.fee_new_icno=a.mem_new_icno ");
            jm_sts1 = DBCon.Ora_Execute_table("select count(mem_new_icno) Bil_ang,ISNULL(SUM(tot_syer) ,'0.00') Jumlah from (select Applicant_Name,wilayah_name,cawangan_name,mem_new_icno,sha_item,tot_syer from (select mem_new_icno,b.wilayah_name,b.cawangan_name,c.Applicant_Name from mem_member m left join Ref_Cawangan b on b.cawangan_code=m.mem_branch_cd and b.wilayah_code=m.mem_region_cd  left join Ref_Applicant_Category C on C.Applicant_Code=m.mem_applicant_type_cd where Acc_sts='Y' ) a inner join (select sha_new_icno,sha_item,sha_debit_amt tot_syer from mem_share where sha_txn_ind='B' and sha_item='TAMBAHAN SYER' and sha_approve_sts_cd='SA' ) b on b.sha_new_icno=a.mem_new_icno) F ");
            jm_sts2 = DBCon.Ora_Execute_table("select count(mem_new_icno) Bil_ang,ISNULL(SUM(tot_set),'0.00')  Jumlah from (select mem_new_icno,set_pay_method_cd,tot_set from (select mem_new_icno from mem_member m where Acc_sts='Y' ) a inner join (select set_new_icno,set_pay_method_cd, set_apply_amt as tot_set  from mem_settlement where Acc_sts='Y' and set_approve_sts_cd='SA' and set_pay_method_cd='TES') b on b.set_new_icno=a.mem_new_icno) F ");
            jm_sts3 = DBCon.Ora_Execute_table("select count(set_new_icno) BilAnggota,ISNULL(sum(set_apply_amt),'0.00') set_apply_amt from (select b.set_new_icno,b.set_apply_amt from (select mem_new_icno from mem_member where Acc_sts='Y' and mem_sts_cd='TS') as a outer apply(select set_new_icno,ISNULL(sum(set_apply_amt),'0.00') set_apply_amt from mem_settlement ms where ms.set_new_icno=a.mem_new_icno and ms.Acc_sts='Y' and ms.set_apply_sts_ind='A' group by set_new_icno) as b ) as a");


            if (get_ahli_sa.Rows.Count != 0)
            {
                dash_txt.Text = get_ahli_sa.Rows[0]["anggota"].ToString();
                dash_txt_amt.Text = double.Parse(get_ahli_sa.Rows[0]["syer"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            }

            if (get_sts_pa.Rows.Count != 0)
            {
                dash_txt2.Text = get_sts_pa.Rows[0]["cnt"].ToString();
                dash_txt2_amt.Text = "0.00";
            }

            if (get_sts_ts.Rows.Count != 0)
            {
                dash_txt3.Text = get_sts_ts.Rows[0]["anggota"].ToString();
                dash_txt3_amt.Text = "0.00";
            }

            if (jm_sts1.Rows.Count != 0)
            {
                dash_txt4.Text = jm_sts1.Rows[0]["Bil_ang"].ToString();
                dash_txt4_1.Text = double.Parse(jm_sts1.Rows[0]["Jumlah"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            }

            if (jm_sts2.Rows.Count != 0)
            {
                dash_txt5.Text = jm_sts2.Rows[0]["Bil_ang"].ToString();
                dash_txt5_1.Text = double.Parse(jm_sts2.Rows[0]["Jumlah"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            }

            if (jm_sts3.Rows.Count != 0)
            {
                dash_txt6.Text = jm_sts3.Rows[0]["BilAnggota"].ToString();
                dash_txt6_1.Text = double.Parse(jm_sts3.Rows[0]["set_apply_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            }

        }
        else if (Session["mnu_id"].ToString().Trim() == "M0003")
        {
            get_ahli_sa = DBCon.Ora_Execute_table("select a.cnt as member,b.cnt as staff,c.cnt as role from (select count(*) cnt from kk_user_login where KK_user_type='Y' and Status='A') as a outer apply(select count(*) cnt from kk_user_login where KK_user_type='N' and Status='A') as b outer apply(select count(*) as cnt from KK_PID_Kumpulan where Status='A') as c");
            if (get_ahli_sa.Rows.Count != 0)
            {
                Label1.Text = get_ahli_sa.Rows[0]["member"].ToString();
                Label2.Text = get_ahli_sa.Rows[0]["staff"].ToString();
                Label3.Text = get_ahli_sa.Rows[0]["role"].ToString();
            }
            module1.Visible = false;
            module2.Visible = true;
            module3.Visible = false;
        }
        else if (Session["mnu_id"].ToString().Trim() == "M0005")
        {
            module1.Visible = false;
            module2.Visible = false;
            module3.Visible = true;
            get_ahli_sa = DBCon.Ora_Execute_table("SELECT count(ast_new_icno) jum_ang, SUM(ISNULL(ast_st_balance_amt,'0.00')) jum_amt FROM aim_st WHERE ISNULL(ast_sahabat_sts_cd,'')='A' ");
            get_sts_pa = DBCon.Ora_Execute_table("SELECT count(ast_new_icno) jum_ang, SUM(ast_st_balance_amt) jum_amt FROM aim_st m1 inner join mem_member m2 on m2.mem_new_icno=m1.ast_new_icno WHERE ISNULL(ast_sahabat_sts_cd,'')='A'  and m2.mem_sts_cd='SA'");
            get_sts_ts = DBCon.Ora_Execute_table("SELECT count(ast_new_icno) jum_ang, SUM(ast_st_balance_amt) jum_amt FROM aim_st m1 inner join mem_member m2 on m2.mem_new_icno=m1.ast_new_icno WHERE ISNULL(ast_sahabat_sts_cd,'')='A'  and m2.mem_sts_cd !='SA'");
           

            if (get_ahli_sa.Rows.Count != 0)
            {
                Label4.Text = get_ahli_sa.Rows[0]["jum_ang"].ToString();
                Label4_1.Text = double.Parse(get_ahli_sa.Rows[0]["jum_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            }

            if (get_sts_pa.Rows.Count != 0)
            {
                Label5.Text = get_sts_pa.Rows[0]["jum_ang"].ToString();
                Label5_1.Text = double.Parse(get_sts_pa.Rows[0]["jum_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            }

            if (get_sts_ts.Rows.Count != 0)
            {
                Label6.Text = get_sts_ts.Rows[0]["jum_ang"].ToString();
                Label6_1.Text = double.Parse(get_sts_ts.Rows[0]["jum_amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
            }
           
        }
        else 
        {
            module1.Visible = false;
            module2.Visible = true;
            module3.Visible = false;
        }
    }

  
}