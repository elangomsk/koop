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

public partial class Jana_Data_pengesahan : System.Web.UI.Page
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
    string status;
    DateTime dmula;
    string bcode;
    string wcode;
    string sno;
    string ccode;
    string Status = string.Empty;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                txtkel.Text = "RE" + DateTime.Now.ToString("yyyyMMdd");
                //Txtfromdate.Attributes.Add("readonly", "readonly");
                //Txttodate.Attributes.Add("readonly", "readonly");
                //txtkel.Attributes.Add("readonly", "readonly");
                Txtkat.Attributes.Add("readonly", "readonly");                
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    void assgn_roles()
    {
        DataTable ddokdicno = new DataTable();
        ddokdicno = DBCon.Ora_Execute_table("select * from KK_User_Login where KK_userid = '" + Session["userid"].ToString() + "'");

        if (ddokdicno.Rows.Count != 0)
        {
            DataTable ddokdicno_1 = new DataTable();
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0121' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();
                //if (role_view == "1")
                //{
                //    Button2.Visible = true;
                //}
                //else
                //{
                //    Button2.Visible = false;
                //}
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
    void app_language()
    {
        if (Session["New"] != null)
        {
            assgn_roles();
            DataTable ste_set = new DataTable();
            ste_set = DBCon.Ora_Execute_table("select * from site_settings where ID IN ('1')");

            DataTable gt_lng = new DataTable();
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('62','63','64','65','66','67','68','883')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;

            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            Button2.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            Button5.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());

        }
        else
        {
            Response.Redirect("../KSAIMB_Login.aspx");
        }
        
    }

    public void showreport_test()
    {
        try
        {
            string script1 = "$(function () { $('[id*=GridView1]').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); });";


            ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);


            string fno = "010102";
            string fname = "JANA PENGESAHAN KEANGGOTAAN";

            string t1 = Txtfromdate.Text;
            string t2 = Txttodate.Text;
            DateTime ft = DateTime.ParseExact(t1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            string fdate = ft.ToString("yyyy-mm-dd");
            DateTime td = DateTime.ParseExact(t2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            string tdate = td.ToString("yyyy-mm-dd");

            //txtkel.Text = "RE" + DateTime.Now.ToString("yyyyMMdd");
            Txtkat.Value = "JANA SENARAI PENGESAHAN UNTUK PENGESAHAN BAGI KELOMPOK" + txtkel.Text;
            DateTime dmula;
            DateTime dakhir;


            dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

            dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

            //string sts = ddpj.SelectedItem.Value;
            string sts = "";
            DateTime today = DateTime.Now;
            //DataSource
            DataTable dt = new DataTable();

            dmula = today;
            dakhir = today;



            // DataTable dt = GetData(DateTime.Parse(datedari), DateTime.Parse(datehingga), nokp, pusat, Caw, Zon, Wil);
            if ((fdate == "") && (tdate == ""))
            {
                dmula = today;
                dakhir = today;

                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                //FromDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                //dt = GetData(fdate, tdate, sts);

                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand("select  m.mem_new_icno,ra.Applicant_Name,m.mem_name,f.fee_txn_dt, case(f.fee_payment_type_cd) when 'P' then 'PST'  when 'Q' then 'CEK'  when 'C' then 'TUNAI'  when 'T' then 'PINDAHAN KE AKAUN BANK' End as fee_payment_type_cd,cast(rw.Wilayah_Name as char(25)) Wilayah_Name,cast(rc.branch_desc as char(25)) as cawangan_name,m.mem_centre,m.mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt, FORMAT(s.sha_txn_dt,'yyyy-MM-dd', 'en-us') as sha_txn_dt from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,f.fee_txn_dt,f.fee_payment_type_cd,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt", con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.Text;

                            sda.Fill(dt);

                            if (dt.Rows.Count != 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {

                                    DataTable ddmem = new DataTable();
                                    ddmem = DBCon.Ora_Execute_table(" update mem_member set mem_batch_name='" + txtkel.Text + "',mem_upd_id='" + Session["New"].ToString() + "',mem_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where mem_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                                    DataTable ddfee = new DataTable();
                                    ddfee = DBCon.Ora_Execute_table(" update mem_fee set fee_batch_name='" + txtkel.Text + "',fee_remark='" + Txtkat.Value + "' where fee_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                                    DataTable ddsha = new DataTable();
                                    ddsha = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtkel.Text + "',sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt='" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");

                                }
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                                service.audit_trail("P0121", "Proses", "Nama Kelompok", txtkel.Text);
                                //gvCustomers.DataSource = dt;
                                //gvCustomers.DataBind();
                                Button1.Visible = true;
                                Button3.Visible = true;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai Dalam Julat Tarikh Yang Dimasukkan.',{'type': 'confirmation','title': 'Warning','auto_close': 2000});", true);
                            }
                        }
                    }
                }

            }
            //date mula ada, date akhir ada
            else if ((fdate != "") && (tdate != ""))
            {
                // dmula = DateTime.ParseExact(datedari, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                // dakhir = DateTime.ParseExact(datehingga, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand("select  m.mem_new_icno,ra.Applicant_Name,m.mem_name,m.mem_name,f.fee_txn_dt, case(f.fee_payment_type_cd) when 'P' then 'PST'  when 'Q' then 'CEK'  when 'C' then 'TUNAI'  when 'T' then 'PINDAHAN KE AKAUN BANK' End as fee_payment_type_cd,cast(rw.Wilayah_Name as char(25)) Wilayah_Name,cast(rc.branch_desc as char(25)) as cawangan_name,m.mem_centre,m.mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt, FORMAT(s.sha_txn_dt,'yyyy-MM-dd', 'en-us') as sha_txn_dt from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_register_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,f.fee_txn_dt,f.fee_payment_type_cd,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt", con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.Text;

                            sda.Fill(dt);

                            if (dt.Rows.Count != 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    DataTable ddmem = new DataTable();
                                    ddmem = DBCon.Ora_Execute_table(" update mem_member set mem_batch_name='" + txtkel.Text + "',mem_upd_id='" + Session["New"].ToString() + "',mem_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where mem_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                                    DataTable ddfee = new DataTable();
                                    ddfee = DBCon.Ora_Execute_table(" update mem_fee set fee_batch_name='" + txtkel.Text + "',fee_remark='" + Txtkat.Value + "' where fee_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                                    DataTable ddsha = new DataTable();
                                    ddsha = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtkel.Text + "',sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt='" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");

                                }
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                                service.audit_trail("P0121", "Proses","Nama Kelompok",txtkel.Text);
                                //gvCustomers.DataSource = dt;
                                //gvCustomers.DataBind();
                                Button1.Visible = true;
                                Button3.Visible = true;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai Dalam Julat Tarikh Yang Dimasukkan.',{'type': 'confirmation','title': 'Warning','auto_close': 2000});", true);
                            }
                        }
                    }
                }

            }
            //date mula ada, date akhir tiada
            else if ((fdate != "") && (tdate == ""))
            {
                //   dmula = DateTime.ParseExact(datedari, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = today;
                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);

                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand("select  m.mem_new_icno,ra.Applicant_Name,m.mem_name,m.mem_name,f.fee_txn_dt, case(f.fee_payment_type_cd) when 'P' then 'PST'  when 'Q' then 'CEK'  when 'C' then 'TUNAI'  when 'T' then 'PINDAHAN KE AKAUN BANK' End as fee_payment_type_cd,cast(rw.Wilayah_Name as char(25)) Wilayah_Name,cast(rc.branch_desc as char(25)) as cawangan_name,m.mem_centre,m.mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt, FORMAT(s.sha_txn_dt,'yyyy-MM-dd', 'en-us') as sha_txn_dt from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_register_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,f.fee_txn_dt,f.fee_payment_type_cd,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt", con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.Text;

                            sda.Fill(dt);
                            if (dt.Rows.Count != 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    DataTable ddmem = new DataTable();
                                    ddmem = DBCon.Ora_Execute_table(" update mem_member set mem_batch_name='" + txtkel.Text + "',mem_upd_id='" + Session["New"].ToString() + "',mem_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where mem_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                                    DataTable ddfee = new DataTable();
                                    ddfee = DBCon.Ora_Execute_table(" update mem_fee set fee_batch_name='" + txtkel.Text + "',fee_remark='" + Txtkat.Value + "' where fee_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                                    DataTable ddsha = new DataTable();
                                    ddsha = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtkel.Text + "',sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt='" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");


                                }
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                                service.audit_trail("P0121", "Proses", "Nama Kelompok", txtkel.Text);
                                //gvCustomers.DataSource = dt;
                                //gvCustomers.DataBind();
                                Button1.Visible = true;
                                Button3.Visible = true;
                            }
                            else
                            {                                
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai Dalam Julat Tarikh Yang Dimasukkan.',{'type': 'confirmation','title': 'Warning','auto_close': 2000});", true);
                            }
                        }
                    }
                }



            }

            else if ((fdate == "") && (tdate != ""))
            {
                dmula = today;
                //  dakhir = DateTime.ParseExact(datehingga, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                // FromDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand("select  m.mem_new_icno,ra.Applicant_Name,m.mem_name,m.mem_name,f.fee_txn_dt, case(f.fee_payment_type_cd) when 'P' then 'PST'  when 'Q' then 'CEK'  when 'C' then 'TUNAI'  when 'T' then 'PINDAHAN KE AKAUN BANK' End as fee_payment_type_cd,cast(rw.Wilayah_Name as char(25)) Wilayah_Name,cast(rc.branch_desc as char(25)) as cawangan_name,m.mem_centre,m.mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt, FORMAT(s.sha_txn_dt,'yyyy-MM-dd', 'en-us') as sha_txn_dt from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_register_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,f.fee_txn_dt,f.fee_payment_type_cd,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt", con))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            cmd.CommandType = CommandType.Text;


                            sda.Fill(dt);
                            if (dt.Rows.Count != 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    DataTable ddmem = new DataTable();
                                    ddmem = DBCon.Ora_Execute_table(" update mem_member set mem_batch_name='" + txtkel.Text + "',mem_upd_id='" + Session["New"].ToString() + "',mem_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where mem_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                                    DataTable ddfee = new DataTable();
                                    ddfee = DBCon.Ora_Execute_table(" update mem_fee set fee_batch_name='" + txtkel.Text + "',fee_remark='" + Txtkat.Value + "' where fee_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                                    DataTable ddsha = new DataTable();
                                    ddsha = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtkel.Text + "' ,sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt='" + dt.Rows[i]["sha_txn_dt"].ToString() + "'  and Acc_sts ='Y'");

                                }
                                GridView1.DataSource = dt;
                                GridView1.DataBind();
                                service.audit_trail("P0121", "Proses", "Nama Kelompok", txtkel.Text);
                                //gvCustomers.DataSource = dt;
                                //gvCustomers.DataBind();
                                Button1.Visible = true;
                                Button3.Visible = true;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai Dalam Julat Tarikh Yang Dimasukkan.',{'type': 'confirmation','title': 'Warning','auto_close': 2000});", true);
                            }
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();
        }

    }

    //public void showreport_test1()
    //{
    //    try
    //    {



    //        string fno = "010102";
    //        string fname = "JANA PENGESAHAN KEANGGOTAAN";

    //        string t1 = Txtfromdate.Text;
    //        string t2 = Txttodate.Text;
    //        DateTime ft = DateTime.ParseExact(t1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
    //        string fdate = ft.ToString("yyyy-mm-dd");
    //        DateTime td = DateTime.ParseExact(t2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
    //        string tdate = td.ToString("yyyy-mm-dd");

    //        txtkel.Text = "RE" + DateTime.Now.ToString("yyyyMMdd");
    //        Txtkat.Value = "JANA SENARAI PENGESAHAN UNTUK PENGESAHAN BAGI KELOMPOK" + "RE" + DateTime.Now.ToString("yyyyMMdd");
    //        DateTime dmula;
    //        DateTime dakhir;


    //        dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

    //        dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

    //        //string sts = ddpj.SelectedItem.Value;
    //        string sts = "";
    //        DateTime today = DateTime.Now;
    //        //DataSource
    //        DataTable dt = new DataTable();

    //        dmula = today;
    //        dakhir = today;



    //        // DataTable dt = GetData(DateTime.Parse(datedari), DateTime.Parse(datehingga), nokp, pusat, Caw, Zon, Wil);
    //        if ((fdate == "") && (tdate == ""))
    //        {
    //            dmula = today;
    //            dakhir = today;

    //            //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
    //            //FromDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
    //            Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
    //            Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
    //            //dt = GetData(fdate, tdate, sts);
    //            using (SqlConnection con = new SqlConnection(conString))
    //            {
    //                using (SqlCommand cmd = new SqlCommand("select  m.mem_new_icno,ra.Applicant_Name,m.mem_name,m.mem_sahabat_no,rw.Wilayah_Name,rc.branch_desc as cawangan_name,m.mem_centre,m.mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt", con))
    //                {
    //                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
    //                    {
    //                        cmd.CommandType = CommandType.Text;



    //                        //int progress = dt.Rows.Count;
    //                        //ProgressPercentage.Text = progress.ToString();
    //                        for (int i = 0; i < dt.Rows.Count; i++)
    //                        {

    //                            DataTable ddmem = new DataTable();
    //                            ddmem = DBCon.Ora_Execute_table(" update mem_member set mem_batch_name='" + txtkel.Text + "',mem_upd_id='" + Session["New"].ToString() + "',mem_upd_dt='" + DateTime.Now.ToString() + "' where mem_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
    //                            DataTable ddfee = new DataTable();
    //                            ddfee = DBCon.Ora_Execute_table(" update mem_fee set fee_batch_name='" + txtkel.Text + "',fee_remark='" + Txtkat.Value + "' where fee_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
    //                            DataTable ddsha = new DataTable();
    //                            ddsha = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtkel.Text + "',sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt='" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");

    //                            string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc)values('" + Session["New"].ToString() + "','" + DateTime.Now.ToString() + "','" + fno + "','" + fname + "')";

    //                            Status = dbcon.Ora_Execute_CommamdText(Inssql);

    //                        }
    //                        sda.Fill(dt);

    //                        gvCustomers.DataSource = dt;
    //                        gvCustomers.DataBind();
    //                    }
    //                }
    //            }

    //        }
    //        //date mula ada, date akhir ada
    //        else if ((fdate != "") && (tdate != ""))
    //        {
    //            // dmula = DateTime.ParseExact(datedari, "MM/dd/yyyy", CultureInfo.InvariantCulture);
    //            // dakhir = DateTime.ParseExact(datehingga, "MM/dd/yyyy", CultureInfo.InvariantCulture);
    //            dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
    //            dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

    //            using (SqlConnection con = new SqlConnection(conString))
    //            {
    //                using (SqlCommand cmd = new SqlCommand("select  m.mem_new_icno,ra.Applicant_Name,m.mem_name,m.mem_sahabat_no,rw.Wilayah_Name,rc.branch_desc as cawangan_name,m.mem_centre,m.mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_register_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt", con))
    //                {
    //                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
    //                    {
    //                        cmd.CommandType = CommandType.Text;

    //                        for (int i = 0; i < dt.Rows.Count; i++)
    //                        {
    //                            DataTable ddmem = new DataTable();
    //                            ddmem = DBCon.Ora_Execute_table(" update mem_member set mem_batch_name='" + txtkel.Text + "',mem_upd_id='" + Session["New"].ToString() + "',mem_upd_dt='" + DateTime.Now.ToString() + "' where mem_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
    //                            DataTable ddfee = new DataTable();
    //                            ddfee = DBCon.Ora_Execute_table(" update mem_fee set fee_batch_name='" + txtkel.Text + "',fee_remark='" + Txtkat.Value + "' where fee_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
    //                            DataTable ddsha = new DataTable();
    //                            ddsha = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtkel.Text + "',sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt='" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");

    //                        }
    //                        sda.Fill(dt);

    //                        gvCustomers.DataSource = dt;
    //                        gvCustomers.DataBind();
    //                    }
    //                }
    //            }

    //        }
    //        //date mula ada, date akhir tiada
    //        else if ((fdate != "") && (tdate == ""))
    //        {
    //            //   dmula = DateTime.ParseExact(datedari, "MM/dd/yyyy", CultureInfo.InvariantCulture);
    //            dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
    //            dakhir = today;
    //            //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
    //            Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);

    //            using (SqlConnection con = new SqlConnection(conString))
    //            {
    //                using (SqlCommand cmd = new SqlCommand("select  m.mem_new_icno,ra.Applicant_Name,m.mem_name,m.mem_sahabat_no,rw.Wilayah_Name,rc.branch_desc as cawangan_name,m.mem_centre,m.mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_register_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt", con))
    //                {
    //                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
    //                    {
    //                        cmd.CommandType = CommandType.Text;

    //                        for (int i = 0; i < dt.Rows.Count; i++)
    //                        {
    //                            DataTable ddmem = new DataTable();
    //                            ddmem = DBCon.Ora_Execute_table(" update mem_member set mem_batch_name='" + txtkel.Text + "',mem_upd_id='" + Session["New"].ToString() + "',mem_upd_dt='" + DateTime.Now.ToString() + "' where mem_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
    //                            DataTable ddfee = new DataTable();
    //                            ddfee = DBCon.Ora_Execute_table(" update mem_fee set fee_batch_name='" + txtkel.Text + "',fee_remark='" + Txtkat.Value + "' where fee_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
    //                            DataTable ddsha = new DataTable();
    //                            ddsha = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtkel.Text + "',sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt='" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");


    //                        }
    //                        sda.Fill(dt);

    //                        gvCustomers.DataSource = dt;
    //                        gvCustomers.DataBind();
    //                    }
    //                }
    //            }



    //        }

    //        else if ((fdate == "") && (tdate != ""))
    //        {
    //            dmula = today;
    //            //  dakhir = DateTime.ParseExact(datehingga, "MM/dd/yyyy", CultureInfo.InvariantCulture);
    //            // FromDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

    //            dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
    //            Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
    //            using (SqlConnection con = new SqlConnection(conString))
    //            {
    //                using (SqlCommand cmd = new SqlCommand("select  m.mem_new_icno,ra.Applicant_Name,m.mem_name,m.mem_sahabat_no,rw.Wilayah_Name,rc.branch_desc as cawangan_name,m.mem_centre,m.mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_register_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt", con))
    //                {
    //                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
    //                    {
    //                        cmd.CommandType = CommandType.Text;

    //                        for (int i = 0; i < dt.Rows.Count; i++)
    //                        {
    //                            DataTable ddmem = new DataTable();
    //                            ddmem = DBCon.Ora_Execute_table(" update mem_member set mem_batch_name='" + txtkel.Text + "',mem_upd_id='" + Session["New"].ToString() + "',mem_upd_dt='" + DateTime.Now.ToString() + "' where mem_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
    //                            DataTable ddfee = new DataTable();
    //                            ddfee = DBCon.Ora_Execute_table(" update mem_fee set fee_batch_name='" + txtkel.Text + "',fee_remark='" + Txtkat.Value + "' where fee_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
    //                            DataTable ddsha = new DataTable();
    //                            ddsha = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtkel.Text + "' ,sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt='" + dt.Rows[i]["sha_txn_dt"].ToString() + "'  and Acc_sts ='Y'");

    //                        }
    //                        sda.Fill(dt);

    //                        gvCustomers.DataSource = dt;
    //                        gvCustomers.DataBind();

    //                    }
    //                }
    //            }


    //        }




    //    }

    //    catch (Exception ex)
    //    {
    //        //txtError.Text = ex.ToString();
    //    }

    //}
   
    protected void ExportToPDF(object sender, EventArgs e)
    {
        try
        {


            string fno = "010102";
            string fname = "JANA PENGESAHAN KEANGGOTAAN";

            string t1 = Txtfromdate.Text;
            string t2 = Txttodate.Text;
            DateTime ft = DateTime.ParseExact(t1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            string fdate = ft.ToString("yyyy-mm-dd");
            DateTime td = DateTime.ParseExact(t2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            string tdate = td.ToString("yyyy-mm-dd");

           
            Txtkat.Value = "JANA SENARAI PENGESAHAN UNTUK PENGESAHAN BAGI KELOMPOK" + "RE" + DateTime.Now.ToString("yyyyMMdd");
            DateTime dmula;
            DateTime dakhir;


            dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

            dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

            //string sts = ddpj.SelectedItem.Value;
            string sts = "";
            DateTime today = DateTime.Now;
            //DataSource
            DataTable dt = new DataTable();

            dmula = today;
            dakhir = today;



            // DataTable dt = GetData(DateTime.Parse(datedari), DateTime.Parse(datehingga), nokp, pusat, Caw, Zon, Wil);
            if ((fdate == "") && (tdate == ""))
            {
                dmula = today;
                dakhir = today;
                Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                //dt = GetData(fdate, tdate, sts);
                dt = DBCon.Ora_Execute_table("select  m.mem_new_icno,ra.Applicant_Name,cast(m.mem_name as char(25)) mem_name,f.fee_txn_dt, case(f.fee_payment_type_cd) when 'P' then 'PST'  when 'Q' then 'CEK'  when 'C' then 'TUNAI'  when 'T' then 'PINDAHAN KE AKAUN BANK' End as fee_payment_type_cd,cast(rw.Wilayah_Name as char(25)) Wilayah_Name,cast(rc.branch_desc as char(25)) as cawangan_name,m.mem_centre,m.mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt,case when sha_reference_ind='P' then 'PST' when sha_reference_ind='C' then 'TUNAI' else '' end as jen_caruman from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,f.fee_txn_dt,f.fee_payment_type_cd,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt,sha_reference_ind");



            }
            //date mula ada, date akhir ada
            else if ((fdate != "") && (tdate != ""))
            {
                dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                //dt = GetData(fdate, tdate, sts);
                dt = DBCon.Ora_Execute_table("select  m.mem_new_icno,cast(m.mem_name as char(25)) mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd,cast(m.mem_centre as char(25)) mem_centre,m.mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt,cast(rw.Wilayah_Name as char(25)) Wilayah_Name,cast(rc.branch_desc as char(25)) as cawangan_name,ra.Applicant_Name,FORMAT(s.sha_txn_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as sha_txn_dt,case when sha_reference_ind='P' then 'PST' when sha_reference_ind='C' then 'TUNAI' else '' end as jen_caruman from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_register_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt,sha_reference_ind");


            }
            //date mula ada, date akhir tiada
            else if ((fdate != "") && (tdate == ""))
            {
                //   dmula = DateTime.ParseExact(datedari, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = today;
                Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                dt = DBCon.Ora_Execute_table("select  m.mem_new_icno,cast(m.mem_name as char(25)) mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd,cast(m.mem_centre as char(25)) mem_centre,m.mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt,cast(rw.Wilayah_Name as char(25)) Wilayah_Name,cast(rc.branch_desc as char(25)) as cawangan_name,ra.Applicant_Name,FORMAT(s.sha_txn_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as sha_txn_dt,case when sha_reference_ind='P' then 'PST' when sha_reference_ind='C' then 'TUNAI' else '' end as jen_caruman from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_register_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt,sha_reference_ind");


            }

            else if ((fdate == "") && (tdate != ""))
            {
                dmula = today;

                dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);

                dt = DBCon.Ora_Execute_table("select  m.mem_new_icno,cast(m.mem_name as char(25)) mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd, cast(m.mem_centre as char(25)) mem_centre,m.mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt,cast(rw.Wilayah_Name as char(25)) Wilayah_Name,cast(rc.branch_desc as char(25)) as cawangan_name,ra.Applicant_Name,FORMAT(s.sha_txn_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as sha_txn_dt,case when sha_reference_ind='P' then 'PST' when sha_reference_ind='C' then 'TUNAI' else '' end as jen_caruman from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_register_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt,sha_reference_ind");


            }



            //  Reset
            ReportViewer1.Reset();

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            if (countRow != 0)
            {
                // txtError.Text = "";
                //Display Report
                ReportDataSource rds = new ReportDataSource("PEN_JANA", dt);

                ReportViewer1.LocalReport.DataSources.Add(rds);

                //Path
                ReportViewer1.LocalReport.ReportPath = "keanggotan/JANA.rdlc";
                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                //Parameters
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("fromDate", Txtfromdate.Text),
                     new ReportParameter("toDate", Txttodate.Text),
                     new ReportParameter("Kelompok", txtkel.Text),
                     //new ReportParameter("Janaan", ddpj.SelectedItem.Text),
                     new ReportParameter("Katerangan", Txtkat.Value),
                     new ReportParameter("current_date", DateTime.Now.ToString())
                     //new ReportParameter("fromDate",dmula.ToShortDateString()  ),
                     //new ReportParameter("toDate",dakhir.ToShortDateString() )
                     };


                ReportViewer1.LocalReport.SetParameters(rptParams);
                ReportViewer1.LocalReport.Refresh();
                string filename = string.Format("{0}.{1}", "Jana_Pengesahan_Keanggotaan_" + DateTime.Now.ToString("ddMMyyyy") + "", "pdf");
                ReportViewer1.LocalReport.DisplayName = "Jana_Pengesahan_Keanggotaan_" + DateTime.Now.ToString("ddMMyyyy");
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

            }
            else if (countRow == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul');", true);

                // txtError.Text = "Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.";
            }
        }

        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();
        }
        string script1 = "$(function () { $('[id*=GridView1]').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }

    protected void ExportToEXCEL(object sender, EventArgs e)
    {
        try
        {


            string fno = "010102";
            string fname = "JANA PENGESAHAN KEANGGOTAAN";

            string t1 = Txtfromdate.Text;
            string t2 = Txttodate.Text;
            DateTime ft = DateTime.ParseExact(t1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            string fdate = ft.ToString("yyyy-mm-dd");
            DateTime td = DateTime.ParseExact(t2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            string tdate = td.ToString("yyyy-mm-dd");

            //txtkel.Text = "RE" + DateTime.Now.ToString("yyyyMMdd");
            Txtkat.Value = "JANA SENARAI PENGESAHAN UNTUK PENGESAHAN BAGI KELOMPOK" + txtkel.Text;
            DateTime dmula;
            DateTime dakhir;


            dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

            dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

            //string sts = ddpj.SelectedItem.Value;
            string sts = "";
            DateTime today = DateTime.Now;
            //DataSource
            DataTable dt = new DataTable();

            dmula = today;
            dakhir = today;



            // DataTable dt = GetData(DateTime.Parse(datedari), DateTime.Parse(datehingga), nokp, pusat, Caw, Zon, Wil);
            if ((fdate == "") && (tdate == ""))
            {
                dmula = today;
                dakhir = today;
                Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                //dt = GetData(fdate, tdate, sts);
                dt = DBCon.Ora_Execute_table("select  m.mem_new_icno,m.mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,substring(ISNULL(REPLACE(REPLACE(m.mem_address,'\r',' '),'\n',' '),''),1,80) mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt,rc.branch_desc as cawangan_name,rw.Wilayah_Name,ra.Applicant_Name,FORMAT(s.sha_txn_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as sha_txn_dt from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt");

            }
            //date mula ada, date akhir ada
            else if ((fdate != "") && (tdate != ""))
            {
                dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                //dt = GetData(fdate, tdate, sts);
                dt = DBCon.Ora_Execute_table("select  m.mem_new_icno,ra.Applicant_Name,m.mem_name,m.mem_name,f.fee_txn_dt, case(f.fee_payment_type_cd) when 'P' then 'PST'  when 'Q' then 'CEK'  when 'C' then 'TUNAI'  when 'T' then 'PINDAHAN KE AKAUN BANK' End as fee_payment_type_cd,rw.Wilayah_Name,rc.branch_desc as cawangan_name,m.mem_centre,substring(ISNULL(REPLACE(REPLACE(m.mem_address,'\r',' '),'\n',' '),''),1,80) mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_register_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,f.fee_txn_dt,f.fee_payment_type_cd,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt");

            }
            //date mula ada, date akhir tiada
            else if ((fdate != "") && (tdate == ""))
            {
                //   dmula = DateTime.ParseExact(datedari, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = today;
                Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                dt = DBCon.Ora_Execute_table("select  m.mem_new_icno,ra.Applicant_Name,m.mem_name,m.mem_name,f.fee_txn_dt, case(f.fee_payment_type_cd) when 'P' then 'PST'  when 'Q' then 'CEK'  when 'C' then 'TUNAI'  when 'T' then 'PINDAHAN KE AKAUN BANK' End as fee_payment_type_cd,rw.Wilayah_Name,rc.branch_desc as cawangan_name,m.mem_centre,substring(ISNULL(REPLACE(REPLACE(m.mem_address,'\r',' '),'\n',' '),''),1,80) mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_register_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,f.fee_txn_dt,f.fee_payment_type_cd,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt");

            }

            else if ((fdate == "") && (tdate != ""))
            {
                dmula = today;

                dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);

                dt = DBCon.Ora_Execute_table("select  m.mem_new_icno,ra.Applicant_Name,m.mem_name,m.mem_name,f.fee_txn_dt, case(f.fee_payment_type_cd) when 'P' then 'PST'  when 'Q' then 'CEK'  when 'C' then 'TUNAI'  when 'T' then 'PINDAHAN KE AKAUN BANK' End as fee_payment_type_cd,rw.Wilayah_Name,rc.branch_desc as cawangan_name,m.mem_centre,substring(ISNULL(REPLACE(REPLACE(m.mem_address,'\r',' '),'\n',' '),''),1,80) mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_register_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + fdate + "'), 0) and DATEADD(day, DATEDIFF(day, 0, '" + tdate + "'), +1) and m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,f.fee_txn_dt,f.fee_payment_type_cd,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt");
            }



            //  Reset
            ReportViewer1.Reset();

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            if (countRow != 0)
            {
                StringBuilder builder = new StringBuilder();
                string strFileName = string.Format("{0}.{1}", "Jana_Pengesahan_Keanggotaan_" + DateTime.Now.ToString("ddMMyyyy") + "", "csv");
                builder.Append("No KP Baru,Kategori,Nama,Tarikh Daftar,Jenis Caruman, Wilayah, Cawangan, Nama Pusat, Alamat, Amaun FI(RM), Amaun Syer(RM)" + Environment.NewLine);
                for (int k = 0; k <= (dt.Rows.Count - 1); k++)
                {

                    builder.Append(dt.Rows[k]["mem_new_icno"].ToString() + " , " + dt.Rows[k]["Applicant_Name"].ToString() + ", " + dt.Rows[k]["mem_name"].ToString() + "," + dt.Rows[k]["fee_txn_dt"].ToString() + "," + dt.Rows[k]["fee_payment_type_cd"].ToString() + "," + dt.Rows[k]["Wilayah_Name"].ToString() + "," + dt.Rows[k]["cawangan_name"].ToString() + "," + dt.Rows[k]["mem_centre"].ToString() + "," + dt.Rows[k]["mem_address"].ToString().Replace(",", "").Replace("\r", " ").Replace("\n", " ") + "," + dt.Rows[k]["fee_amount"].ToString().Replace(",", "") + "," + dt.Rows[k]["sha_debit_amt"].ToString().Replace(",", "") + Environment.NewLine);

                }
                Response.Clear();
                Response.ContentType = "text/csv";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                Response.Write(builder.ToString());
                Response.End();

            }
            else if (countRow == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul');", true);

                // txtError.Text = "Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.";
            }
        }

        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();
        }
        string script1 = "$(function () { $('[id*=GridView1]').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); });";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);
    }



    public void showreport()
    {
        try
        {
            string fno = "010102";
            string fname = "JANA PENGESAHAN KEANGGOTAAN";

            string t1 = Txtfromdate.Text;
            string t2 = Txttodate.Text;
            DateTime ft = DateTime.ParseExact(t1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            string fdate = ft.ToString("yyyy-mm-dd");
            DateTime td = DateTime.ParseExact(t2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            string tdate = td.ToString("yyyy-mm-dd");

            txtkel.Text = "RE" + DateTime.Now.ToString("yyyyMMdd");
            Txtkat.Value = "JANA SENARAI PENGESAHAN UNTUK PENGESAHAN BAGI KELOMPOK" + "RE" + DateTime.Now.ToString("yyyyMMdd");
            DateTime dmula;
            DateTime dakhir;


            dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

            dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);

            //string sts = ddpj.SelectedItem.Value;
            string sts = "";
            DateTime today = DateTime.Now;
            //DataSource
            DataTable dt = new DataTable();

            dmula = today;
            dakhir = today;



            // DataTable dt = GetData(DateTime.Parse(datedari), DateTime.Parse(datehingga), nokp, pusat, Caw, Zon, Wil);
            if ((fdate == "") && (tdate == ""))
            {
                dmula = today;
                dakhir = today;

                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                //FromDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                dt = DBCon.Ora_Execute_table("select  m.mem_new_icno,m.mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,f.fee_amount,sum(s.sha_debit_amt) as sha_debit_amt,rc.branch_desc as cawangan_name,rw.Wilayah_Name,ra.Applicant_Name,FORMAT(s.sha_txn_dt,'yyyy-MM-dd HH:mm:ss', 'en-us') as sha_txn_dt from mem_member AS m Left Join Ref_Applicant_Category ra on ra.Applicant_Code=m.mem_applicant_type_cd Left join ref_branch AS rc on rc.branch_cd=m.mem_branch_cd Left join Ref_Wilayah AS rw on rw.wilayah_code=m.mem_region_cd Left join mem_fee AS f on m.mem_new_icno=f.fee_new_icno and f.Acc_sts ='Y' inner join mem_share AS s on s.sha_new_icno=m.mem_new_icno and s.Acc_sts ='Y' where m.mem_sts_cd='' and m.Acc_sts ='Y' and ISNUll(s.sha_refund_ind,'') ='' and ISNULL(f.fee_refund_ind,'')='' and ISNULL(sha_approve_sts_cd,'') = '' group by m.mem_new_icno,m.mem_name,m.mem_sahabat_no,m.mem_region_cd,m.mem_branch_cd,m.mem_centre,m.mem_address,rc.branch_desc,rw.Wilayah_Name,f.fee_amount,ra.Applicant_Name,s.sha_txn_dt");
                //dt = GetData(fdate, tdate);

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    DataTable ddmem = new DataTable();
                    ddmem = DBCon.Ora_Execute_table(" update mem_member set mem_batch_name='" + txtkel.Text + "',mem_upd_id='" + Session["New"].ToString() + "',mem_upd_dt='" +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where mem_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                    DataTable ddfee = new DataTable();
                    ddfee = DBCon.Ora_Execute_table(" update mem_fee set fee_batch_name='" + txtkel.Text + "',fee_remark='" + Txtkat.Value + "' where fee_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                    DataTable ddsha = new DataTable();
                    ddsha = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtkel.Text + "',sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt='" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");

                    string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc)values('" + Session["New"].ToString() + "','" +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + fno + "','" + fname + "')";

                    Status = dbcon.Ora_Execute_CommamdText(Inssql);
                }

            }
            //date mula ada, date akhir ada
            else if ((fdate != "") && (tdate != ""))
            {
                // dmula = DateTime.ParseExact(datedari, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                // dakhir = DateTime.ParseExact(datehingga, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dt = GetData(fdate, tdate, sts);
                //dt = GetData(fdate, tdate);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable ddmem = new DataTable();
                    ddmem = DBCon.Ora_Execute_table(" update mem_member set mem_batch_name='" + txtkel.Text + "',mem_upd_id='" + Session["New"].ToString() + "',mem_upd_dt='" +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where mem_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                    DataTable ddfee = new DataTable();
                    ddfee = DBCon.Ora_Execute_table(" update mem_fee set fee_batch_name='" + txtkel.Text + "',fee_remark='" + Txtkat.Value + "' where fee_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                    DataTable ddsha = new DataTable();
                    ddsha = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtkel.Text + "',sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt='" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");

                    string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc)values('" + Session["New"].ToString() + "','" +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + fno + "','" + fname + "')";

                    Status = dbcon.Ora_Execute_CommamdText(Inssql);
                }
            }
            //date mula ada, date akhir tiada
            else if ((fdate != "") && (tdate == ""))
            {
                //   dmula = DateTime.ParseExact(datedari, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                dmula = DateTime.ParseExact(fdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                dakhir = today;
                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                Txttodate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);
                //("MM/dd/yyyy HH:mm:ss.fff",
                //     CultureInfo.InvariantCulture);
                dt = GetData(fdate, tdate, sts);
                //dt = GetData(fdate, tdate);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable ddmem = new DataTable();
                    ddmem = DBCon.Ora_Execute_table(" update mem_member set mem_batch_name='" + txtkel.Text + "',mem_upd_id='" + Session["New"].ToString() + "',mem_upd_dt='" +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where mem_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                    DataTable ddfee = new DataTable();
                    ddfee = DBCon.Ora_Execute_table(" update mem_fee set fee_batch_name='" + txtkel.Text + "',fee_remark='" + Txtkat.Value + "' where fee_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                    DataTable ddsha = new DataTable();
                    ddsha = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtkel.Text + "',sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt='" + dt.Rows[i]["sha_txn_dt"].ToString() + "' and Acc_sts ='Y'");

                    string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc)values('" + Session["New"].ToString() + "','" +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + fno + "','" + fname + "')";

                    Status = dbcon.Ora_Execute_CommamdText(Inssql);
                }
            }

            else if ((fdate == "") && (tdate != ""))
            {
                dmula = today;
                //  dakhir = DateTime.ParseExact(datehingga, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                // FromDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                dakhir = DateTime.ParseExact(tdate, "yyyy-mm-dd", CultureInfo.InvariantCulture);
                Txtfromdate.Text = today.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture);

                //kena add exception error, date akhir tak boleh previous dr date mula

                dt = GetData(fdate, tdate, sts);
                //dt = GetData(fdate, tdate);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataTable ddmem = new DataTable();
                    ddmem = DBCon.Ora_Execute_table(" update mem_member set mem_batch_name='" + txtkel.Text + "',mem_upd_id='" + Session["New"].ToString() + "',mem_upd_dt='" +DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where mem_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                    DataTable ddfee = new DataTable();
                    ddfee = DBCon.Ora_Execute_table(" update mem_fee set fee_batch_name='" + txtkel.Text + "',fee_remark='" + Txtkat.Value + "' where fee_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");
                    DataTable ddsha = new DataTable();
                    ddsha = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtkel.Text + "' ,sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + dt.Rows[i][0].ToString() + "' and sha_txn_dt='" + dt.Rows[i]["sha_txn_dt"].ToString() + "'  and Acc_sts ='Y'");
                    //ddsha = DBCon.Ora_Execute_table("update mem_share set sha_batch_name='" + txtkel.Text + "' ,sha_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',sha_upd_id='" + Session["New"].ToString() + "' where sha_new_icno='" + dt.Rows[i][0].ToString() + "' and Acc_sts ='Y'");

                    string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc)values('" + Session["New"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + fno + "','" + fname + "')";

                    Status = dbcon.Ora_Execute_CommamdText(Inssql);


                }
            }

            //Reset
            ReportViewer1.Reset();

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            if (countRow != 0)
            {
                disp_hdr_txt.Visible = false;
                  //Display Report
                  ReportDataSource rds = new ReportDataSource("PEN_JANA", dt);

                ReportViewer1.LocalReport.DataSources.Add(rds);

                //Path
                ReportViewer1.LocalReport.ReportPath = "keanggotan/JANA.rdlc";
                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                //Parameters
                ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("fromDate", Txtfromdate.Text),
                     new ReportParameter("toDate", Txttodate.Text),
                     new ReportParameter("Kelompok", txtkel.Text),
                     //new ReportParameter("Janaan", ddpj.SelectedItem.Text),
                     new ReportParameter("Katerangan", Txtkat.Value),
                     new ReportParameter("current_date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                     //new ReportParameter("fromDate",dmula.ToShortDateString()  ),
                     //new ReportParameter("toDate",dakhir.ToShortDateString() )
                     };


                ReportViewer1.LocalReport.SetParameters(rptParams);

                //Refresh
                ReportViewer1.LocalReport.Refresh();
                System.Threading.Thread.Sleep(1);
            }
            else if (countRow == 0)
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);

                // txtError.Text = "Rekod tidak dijumpai. Sila pastikan semua maklumat dimasukkan dengan betul.";
            }
        }

        catch (Exception ex)
        {
            //txtError.Text = ex.ToString();
        }

    }




    private DataTable GetData(string fromDate, string toDate, string wilayah)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ToString();
        using (SqlConnection cn = new SqlConnection(constr))
        {
            SqlCommand cmd = new SqlCommand("JANA_PEN", cn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@frodate", SqlDbType.DateTime).Value = fromDate;
            cmd.Parameters.Add("@todate", SqlDbType.DateTime).Value = toDate;
            cmd.Parameters.Add("@sts", SqlDbType.VarChar).Value = wilayah;
            cmd.Parameters.Add("@cdate", SqlDbType.DateTime).Value =DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            cmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = Session["New"].ToString();
            //cmd.Parameters.Add("@acname", SqlDbType.VarChar).Value = acname;



            //cmd.Parameters.Add("@shbtid", SqlDbType.VarChar).Value = shbtid;
            //cmd.Parameters.Add("@shbtnama", SqlDbType.VarChar).Value = namashbt;

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);

        }
        return dt;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Txtfromdate.Text != "")
        {
            if (Txttodate.Text != "")
            {
                showreport_test();
            }
            else
            {                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }
        }

        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
}