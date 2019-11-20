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
using System.Threading;

public partial class Sample_print : System.Web.UI.Page
{
    DBConnection dbcon = new DBConnection();
    DBConnection DBCon = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable dt = new DataTable();
    string useid = string.Empty;
    string Status = string.Empty;
    string oid = string.Empty;
    string sdd = string.Empty;
    string CommandArgument1 = string.Empty, CommandArgument2 = string.Empty, CommandArgument3 = string.Empty, CommandArgument4 = string.Empty, CommandArgument5 = string.Empty, CommandArgument6 = string.Empty, CommandArgument7 = string.Empty;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        string script = " $(function () {$('.select2').select2()});";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script, true);
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.Btn_Cetak);
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                f_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                t_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                useid = Session["New"].ToString();
                kod();
                
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('160','1052','1106','1107','64','65','22','23','108','36','1042')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;
            
            //ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            //ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
            //ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Btn_Cetak.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button3.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());

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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0149' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();
                if (role_add == "1")
                {
                    Btn_Cetak.Visible = true;
                }
                else
                {
                    Btn_Cetak.Visible = false;
                }

            }
        }
    }


    void kod()
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select UPPER(kavasan_name) as kavasan_name from Ref_Cawangan group by kavasan_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Kategori.DataSource = dt;
            DD_Kategori.DataBind();
            DD_Kategori.DataTextField = "kavasan_name";
            DD_Kategori.DataValueField = "kavasan_name";
            DD_Kategori.DataBind();
            DD_Kategori.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void DD_Sub_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select cawangan_code,UPPER(cawangan_name) as cawangan_name from Ref_Cawangan where kavasan_name='" + DD_Kategori.SelectedItem.Text + "' and wilayah_name='" + DD_Sub.SelectedItem.Text + "' group by cawangan_code,cawangan_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DropDownList1.DataSource = dt;
            DropDownList1.DataBind();
            DropDownList1.DataTextField = "cawangan_name";
            DropDownList1.DataValueField = "cawangan_code";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void DD_Kategori_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet Ds = new DataSet();
        try
        {

            string com = "select wilayah_code,UPPER(wilayah_name) as wilayah_name from Ref_Cawangan where kavasan_name='" + DD_Kategori.SelectedItem.Text + "' group by wilayah_code,wilayah_name";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_Sub.DataSource = dt;
            DD_Sub.DataBind();
            DD_Sub.DataTextField = "wilayah_name";
            DD_Sub.DataValueField = "wilayah_code";
            DD_Sub.DataBind();
            DD_Sub.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void rst_clk(object sender, EventArgs e)
    {
        Response.Redirect("../keanggotan/Sample_print.aspx");
    }

    protected void Btn_Cetak_Click(object sender, EventArgs e)
    {
        if (DD_Kategori.SelectedValue != "" && DD_Sub.SelectedValue != "")
        {
            string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            
            {

                string fdate = f_date.Text;
                DateTime fd = DateTime.ParseExact(fdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                String fmdate = fd.ToString("yyyy-MM-dd");

                string tdate = t_date.Text;
                DateTime td = DateTime.ParseExact(tdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                String tmdate = td.ToString("yyyy-MM-dd");

                string sql;
                if (DropDownList1.SelectedValue == "")
                {
                    sql = "select mem_new_icno from mem_member where mem_sts_cd='SA' and    mem_region_cd='" + DD_Sub.SelectedItem.Value + "' and Acc_sts='Y' and mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1)  order by mem_branch_cd,mem_centre ";
                }
                else
                {
                    sql = "select mem_new_icno from mem_member where mem_sts_cd='SA' and  mem_branch_cd='" + DropDownList1.SelectedItem.Value + "' and Acc_sts='Y' and mem_register_dt>=DATEADD(day, DATEDIFF(day, 0, '" + fmdate + "'), 0) and mem_register_dt<=DATEADD(day, DATEDIFF(day, 0, '" + tmdate + "'), +1) order by mem_branch_cd,mem_centre ";
                }
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);

                            //Build the Text file data.
                            string txt = string.Empty;

                            //foreach (DataColumn column in dt.Columns)
                            //{
                            //    //Add the Header row for Text file.
                            //    txt += column.ColumnName + "\t\t";
                            //}

                            //Add new line.
                            //txt += "\r\n";
                            if (dt.Rows.Count != 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    DataTable gt_name = new DataTable();
                                    gt_name = dbcon.Ora_Execute_table("select mem_name from mem_member where mem_sts_cd='SA' and Acc_sts='Y' and mem_new_icno='" + row["mem_new_icno"].ToString() + "'");

                                    string name = row["mem_new_icno"].ToString();
                                    string name1 = gt_name.Rows[0]["mem_name"].ToString().Replace("'", "''");

                                    foreach (DataColumn column in dt.Columns)
                                    {
                                        //Add the Data rows.
                                        //txt += row[column.ColumnName].ToString() + "\t\t";
                                        string s1 = "(select (a.STUNAI) - (c.STUNAI) as STUNAI  from (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_debit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + name + "' and Acc_sts ='Y' and sha_refund_ind='N' group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final )a full outer join (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_credit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + name + "' and Acc_sts ='Y' and sha_refund_ind='N' group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final ) c on c.sha_new_icno=a.sha_new_icno full outer join (select * from (select isnull([FTUNAI],'') as FTUNAI,isnull([FPST],'') as FPST,fee_new_icno from (select  SUM(fee_amount) as Tran_count, case (fee_payment_type_cd) WHEN 'C' THEN 'FTUNAI' WHEN 'P' THEN 'FPST' END MONTHNAME,fee_new_icno from mem_fee where fee_new_icno='" + name + "' and Acc_sts ='Y' and fee_refund_ind='N'  group by fee_payment_type_cd,fee_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([FTUNAI], [FPST]))AS PivotTable) as final )b on b.fee_new_icno=c.sha_new_icno and b.fee_new_icno='" + name + "')";
                                        string s2 = "(select(a.SPST) - (c.SPST) as SPST  from (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_debit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + name + "' and Acc_sts ='Y' and sha_refund_ind='N' group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final )a full outer join (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_credit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + name + "' and Acc_sts ='Y' and sha_refund_ind='N' group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final ) c on c.sha_new_icno=a.sha_new_icno full outer join (select * from (select isnull([FTUNAI],'') as FTUNAI,isnull([FPST],'') as FPST,fee_new_icno from (select  SUM(fee_amount) as Tran_count, case (fee_payment_type_cd) WHEN 'C' THEN 'FTUNAI' WHEN 'P' THEN 'FPST' END MONTHNAME,fee_new_icno from mem_fee where fee_new_icno='" + name + "' and Acc_sts ='Y' and fee_refund_ind='N'  group by fee_payment_type_cd,fee_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([FTUNAI], [FPST]))AS PivotTable) as final )b on b.fee_new_icno=c.sha_new_icno and b.fee_new_icno='" + name + "')";
                                        string s3 = "(select (a.STUNAI + a.SPST) - (c.STUNAI + c.SPST) as Jumlah  from (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_debit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + name + "' and Acc_sts ='Y' and sha_refund_ind='N' group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final )a full outer join (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_credit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + name + "' and Acc_sts ='Y' and sha_refund_ind='N' group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final ) c on c.sha_new_icno=a.sha_new_icno full outer join (select * from (select isnull([FTUNAI],'') as FTUNAI,isnull([FPST],'') as FPST,fee_new_icno from (select  SUM(fee_amount) as Tran_count, case (fee_payment_type_cd) WHEN 'C' THEN 'FTUNAI' WHEN 'P' THEN 'FPST' END MONTHNAME,fee_new_icno from mem_fee where fee_new_icno='" + name + "' and Acc_sts ='Y' and fee_refund_ind='N'  group by fee_payment_type_cd,fee_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([FTUNAI], [FPST]))AS PivotTable) as final )b on b.fee_new_icno=c.sha_new_icno and b.fee_new_icno='" + name + "')";
                                        string s4 = string.Empty, s5 = string.Empty;
                                        DataTable cnt_no2 = new DataTable();
                                        cnt_no2 = dbcon.Ora_Execute_table("select ISNULL(Format(ast_end_date,'dd/MM/yyyy'),'') end_dt,ISNULL(ast_st_balance_amt,'0.00') amt from aim_st where ast_new_icno ='" + name + "'");
                                        if (cnt_no2.Rows.Count != 0)
                                        {
                                            s4 = cnt_no2.Rows[0]["end_dt"].ToString();
                                            s5 = double.Parse(cnt_no2.Rows[0]["amt"].ToString()).ToString("C").Replace("$", "").Replace("RM", "");
                                        }
                                        else
                                        {
                                            s5 = "0.00";
                                        }
                                        using (SqlCommand cmd1 = new SqlCommand("select CONCAT('A001',cast('" + DateTime.Now.ToString("dd/MM/yyyy") + "' as char(10)),mem_member_no,SPACE(12-len(left(mem_member_no ,12))), substring(ISNULL(mem_centre,''),1,40),SPACE(40-len(left(substring(ISNULL(mem_centre,''),1,40) ,40))),substring(ISNULL(cawangan_name,''),1,40),SPACE(40-len(left(substring(ISNULL(cawangan_name,''),1,40) ,40))),substring(ISNULL(wilayah_name,''),1,40),SPACE(40-len(left(substring(ISNULL(wilayah_name,''),1,40) ,40))),substring(ISNULL('" + name1 + "',''),1,40),SPACE(40-len(left(substring(ISNULL('" + name1 + "',''),1,40) ,40))),substring(ISNULL(REPLACE(REPLACE(mem_address,'\r',' '),'\n',' '),''),1,80),SPACE(80 - len(left(substring(ISNULL(REPLACE(REPLACE(mem_address,'\r',' '),'\n',' '),''),1,80) ,80))),substring(ISNULL(mem_postcd,''),1,10),SPACE(10-len(left(mem_postcd ,10))),substring(ISNULL(ren.Decription,''),1,30),SPACE(30-len(left(substring(ISNULL(ren.Decription,''),1,30) ,30))),substring(ISNULL('" + s4 + "',''),1,10),SPACE(10-len(left(substring(ISNULL('" + s4 + "',''),1,10) ,10))),SPACE(15-len(left(substring(ISNULL('" + s5 + "',''),1,15),15))),substring(ISNULL('" + s5 + "',''),1,15)," + name + ",SPACE(12-len(left(ISNULL(" + name + ",''),12))),ISNULL(case when ISNULL(mem_phone_h,'') = '' and ISNULL(mem_phone_o,'') = '' then ISNULL(mem_phone_m,'') when ISNULL(mem_phone_m,'') = '' and ISNULL(mem_phone_o,'') = '' then ISNULL(mem_phone_h,'') when ISNULL(mem_phone_h,'') = '' and ISNULL(mem_phone_m,'') = '' then ISNULL(mem_phone_o,'') end ,''),SPACE(12-len(left(ISNULL(case when ISNULL(mem_phone_h,'') = '' and ISNULL(mem_phone_o,'') = '' then ISNULL(mem_phone_m,'') when ISNULL(mem_phone_m,'') = '' and ISNULL(mem_phone_o,'') = '' then ISNULL(mem_phone_h,'') when ISNULL(mem_phone_h,'') = '' and ISNULL(mem_phone_m,'') = '' then ISNULL(mem_phone_o,'') end ,''),12))),format(mem_register_dt,'dd/MM/yyyy'),SPACE(10-len(left(format(mem_register_dt,'dd/MM/yyyy'),10))),SPACE(15-len(left(ISNULL(" + s1 + ",'0.00'),15)))," + s1 + ",SPACE(15-len(left(ISNULL(" + s2 + ",'0.00'),15)))," + s2 + ",SPACE(15-len(left(ISNULL(" + s3 + ",'0.00'),15)))," + s3 + ")  FROM mem_member mm1 INNER JOIN mem_fee mf3 ON mf3.fee_new_icno=mm1.mem_new_icno and mf3.Acc_sts='Y' LEFT JOIN aim_st ON ast_new_icno=mem_new_icno left join Ref_Cawangan on cawangan_code=mem_branch_cd and mem_region_cd=wilayah_code  left join Ref_Kawasan on Area_Code=mem_area_cd and cawangan_cd=mem_branch_cd and wilayah_cd=mem_region_cd left join Ref_Applicant_Category as rac on rac.Applicant_Code=mem_applicant_type_cd left join Ref_Negeri ren on ren.Decription_Code=mem_negri WHERE mm1.mem_new_icno='" + name + "' and mm1.Acc_sts='Y' and mm1.mem_sts_cd='SA'"))
                                        {
                                            using (SqlDataAdapter sda1 = new SqlDataAdapter())
                                            {
                                                cmd1.Connection = con;
                                                sda1.SelectCommand = cmd1;
                                                using (DataTable dt1 = new DataTable())
                                                {
                                                    sda1.Fill(dt1);

                                                    //Build the Text file data.
                                                    //string txt = string.Empty;

                                                    //foreach (DataColumn column in dt.Columns)
                                                    //{
                                                    //    //Add the Header row for Text file.
                                                    //    txt += column.ColumnName + "\t\t";
                                                    //}

                                                    //Add new line.
                                                    txt += "\r\n";

                                                    foreach (DataRow row1 in dt1.Rows)
                                                    {
                                                        foreach (DataColumn column1 in dt1.Columns)
                                                        {
                                                            //Add the Data rows.
                                                            txt += row1[column1.ColumnName].ToString();
                                                            txt += "\r\n";
                                                        }

                                                        //Add new line.

                                                    }
                                                    using (SqlCommand cmd2 = new SqlCommand("select CONCAT( cast('S00'+CAST(rank()  OVER (ORDER BY sha_txn_dt, sha_reference_no) as varchar(30)) as char(4)),format(sha_txn_dt,'dd/MM/yyyy') ,SPACE(10-len(left(format(sha_txn_dt,'dd/MM/yyyy'),10))),substring(ISNULL(sha_item,''),1,100) , SPACE(100-len(left(substring(ISNULL(sha_item,''),1,100),100))),case when ISNULL(sha_reference_ind,'') = 'P' then 'PST' when ISNULL(sha_reference_ind,'') = 'C' then 'TUNAI' else '' end , SPACE(30-len(left((case when ISNULL(sha_reference_ind,'') = 'P' then 'PST' when ISNULL(sha_reference_ind,'') = 'C' then 'TUNAI' else '' end),30))), SPACE(15-len(left(sha_debit_amt,15))),sha_debit_amt , SPACE(15-len(left(sha_credit_amt,15))),sha_credit_amt, SPACE(15-len(left((sha_debit_amt-sha_credit_amt),15))),(sha_debit_amt-sha_credit_amt)) from mem_share ms1 where ms1.sha_new_icno='" + name + "' and ms1.Acc_sts='Y' and ms1.sha_approve_sts_cd='SA' order by sha_txn_dt"))
                                                    {
                                                        using (SqlDataAdapter sda2 = new SqlDataAdapter())
                                                        {
                                                            cmd2.Connection = con;
                                                            sda2.SelectCommand = cmd2;
                                                            using (DataTable dt2 = new DataTable())
                                                            {
                                                                sda2.Fill(dt2);

                                                                //Build the Text file data.
                                                                //string txt = string.Empty;

                                                                //foreach (DataColumn column in dt.Columns)
                                                                //{
                                                                //    //Add the Header row for Text file.
                                                                //    txt += column.ColumnName + "\t\t";
                                                                //}

                                                                //Add new line.
                                                                //txt += "\r\n";

                                                                foreach (DataRow row2 in dt2.Rows)
                                                                {
                                                                    foreach (DataColumn column2 in dt2.Columns)
                                                                    {
                                                                        //Add the Data rows.
                                                                        txt += row2[column2.ColumnName].ToString();
                                                                        txt += "\r\n";
                                                                    }

                                                                    //Add new line.

                                                                }
                                                                using (SqlCommand cmd3 = new SqlCommand("select CONCAT(cast('D00'+CAST(rank()  OVER (ORDER BY div_process_dt, div_bank_cd) as varchar(30)) as char(4)),format(div_process_dt,'dd/MM/yyyy'),SPACE(10-len(left(format(div_process_dt,'dd/MM/yyyy'),10))),substring(ISNULL(div_remark,''),1,100),SPACE(100-len(left(substring(ISNULL(div_remark,''),1,100),100))),substring(ISNULL(rnb.Bank_Name,''),1,30),SPACE(30-len(left(substring(ISNULL(rnb.Bank_Name,''),1,30),30))),substring(ISNULL(div_bank_acc_no,''),1,20),SPACE(20-len(left(substring(ISNULL(div_bank_acc_no,''),1,20),20))), SPACE(15-len(left(div_debit_amt,15))),ISNULL(div_debit_amt,'0.00'), SPACE(15-len(left(div_debit_amt,15))),ISNULL(div_debit_amt,'0.00')) from mem_divident md1 left join Ref_Nama_Bank as rnb on rnb.Bank_Code=md1.div_bank_cd where md1.div_new_icno='" + name + "' and md1.Acc_sts='Y' and div_approve_ind='SA'"))
                                                                {
                                                                    using (SqlDataAdapter sda3 = new SqlDataAdapter())
                                                                    {
                                                                        cmd3.Connection = con;
                                                                        sda3.SelectCommand = cmd3;
                                                                        using (DataTable dt3 = new DataTable())
                                                                        {
                                                                            sda3.Fill(dt3);

                                                                            //Build the Text file data.
                                                                            //string txt = string.Empty;

                                                                            //foreach (DataColumn column in dt.Columns)
                                                                            //{
                                                                            //    //Add the Header row for Text file.
                                                                            //    txt += column.ColumnName + "\t\t";
                                                                            //}

                                                                            //Add new line.
                                                                            //txt += "\r\n";

                                                                            foreach (DataRow row3 in dt3.Rows)
                                                                            {
                                                                                foreach (DataColumn column3 in dt3.Columns)
                                                                                {
                                                                                    //Add the Data rows.
                                                                                    txt += row3[column3.ColumnName].ToString().Trim();
                                                                                    txt += "\r\n";
                                                                                }

                                                                                //Add new line.

                                                                            }


                                                                        }
                                                                    }
                                                                }

                                                            }
                                                        }
                                                    }


                                                }
                                            }
                                        }
                                    }

                                    //Add new line.

                                }
                                string filename1 = string.Empty;
                                if (DropDownList1.SelectedValue == "")
                                {
                                    filename1 = "Wilayah_" + DD_Sub.SelectedItem.Text;
                                }
                                else
                                {
                                    filename1 = "Cawangan_" + DropDownList1.SelectedItem.Text;
                                }
                                //Download the Text file.
                                Response.Clear();
                                Response.Buffer = true;
                                Response.AddHeader("content-disposition", "attachment;filename=" + filename1 + ".txt");
                                Response.Charset = "";
                                Response.ContentType = "application/text";
                                Response.Output.Write(txt);
                                
                                //Response.Flush();
                                Response.End();

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukan Input Carian.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
}