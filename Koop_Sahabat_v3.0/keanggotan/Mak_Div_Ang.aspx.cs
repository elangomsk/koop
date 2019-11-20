using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.Threading;

public partial class Mak_Div_Ang : System.Web.UI.Page
{
    DBConnection Con = new DBConnection();
    string cs = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DBConnection DBCon = new DBConnection();
    StudentWebService service = new StudentWebService();
    string Status = string.Empty;
    string userid;
    string level;
    int i = 0;
    string role_1 = string.Empty, role_view = string.Empty, role_add = string.Empty, role_edit = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app_language();
      
        if (!this.IsPostBack)
        {
            if (Session["New"] != null)
            {
                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                //fromdate.Attributes.Add("readonly", "readonly");
                //todate.Attributes.Add("readonly", "readonly");
                string batch_code = DateTime.Now.ToString("yyyyMMdd");
                kelompok.Text = "DV" + batch_code;
                remark.Value = "DIVIDEN BAGI TAHUN " + DateTime.Now.Year;
                s_anggota.SelectedIndex = 1;
             
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
            gt_lng = DBCon.Ora_Execute_table("select " + Session["site_languaage"].ToString() + " from Ref_language where ID IN ('128','1052','1078','129','64','65','21','130','53','66','68','1042')");

            CultureInfo culinfo = Thread.CurrentThread.CurrentCulture;
            TextInfo txtinfo = culinfo.TextInfo;




            ps_lbl1.Text = txtinfo.ToTitleCase(gt_lng.Rows[6][0].ToString().ToLower());
            ps_lbl2.Text = txtinfo.ToTitleCase(gt_lng.Rows[10][0].ToString().ToLower());
            ps_lbl3.Text = txtinfo.ToTitleCase(gt_lng.Rows[11][0].ToString().ToLower());
            ps_lbl4.Text = txtinfo.ToTitleCase(gt_lng.Rows[2][0].ToString().ToLower());
            ps_lbl5.Text = txtinfo.ToTitleCase(gt_lng.Rows[0][0].ToString().ToLower());
            ps_lbl6.Text = txtinfo.ToTitleCase(gt_lng.Rows[3][0].ToString().ToLower());
            ps_lbl7.Text = txtinfo.ToTitleCase(gt_lng.Rows[8][0].ToString().ToLower());
            ps_lbl8.Text = txtinfo.ToTitleCase(gt_lng.Rows[7][0].ToString().ToLower());
            ps_lbl9.Text = txtinfo.ToTitleCase(gt_lng.Rows[4][0].ToString().ToLower());
            ps_lbl10.Text = txtinfo.ToTitleCase(gt_lng.Rows[5][0].ToString().ToLower());
            Button1.Text = txtinfo.ToTitleCase(gt_lng.Rows[1][0].ToString().ToLower());
            Button6.Text = txtinfo.ToTitleCase(gt_lng.Rows[9][0].ToString().ToLower());
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
            ddokdicno_1 = DBCon.Ora_Execute_table("select m1.*,s1.ctrl_type from KK_Role_skrins m1 left join KK_PID_Kumpulan s1 on s1.KK_kumpulan_id=Role_id  where psub_skrin_id='P0137' and Role_id IN ('" + ddokdicno.Rows[0]["KK_roles"].ToString().Replace(",", "','") + "')");

            if (ddokdicno_1.Rows.Count != 0)
            {
                role_1 = ddokdicno_1.Rows[0]["ctrl_type"].ToString();
                role_view = ddokdicno_1.Rows[0]["view_chk"].ToString();
                role_add = ddokdicno_1.Rows[0]["Add_chk"].ToString();
                role_edit = ddokdicno_1.Rows[0]["edit_chk"].ToString();

                if (role_add == "1")
                {
                    Button1.Visible = true;
                }
                else
                {
                    Button1.Visible = false;
                }

            }
        }
    }




    protected void Button1_Click(object sender, EventArgs e)
    {
        if (fromdate.Text != "" && todate.Text != "" && TextBox1.Text != "")
        {
            
            ShowReport_test();
        }
        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Sila Masukkan Input Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }


    private DataTable GetData(string datedari, string datehingga, string sanggota, string kelompok_name, string Peratusan_dividen)
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ToString();
        using (SqlConnection cn = new SqlConnection(constr))
        {
            //SqlCommand cmd = new SqlCommand("mda_procedure", cn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = datedari;
            //cmd.Parameters.Add("@todate", SqlDbType.DateTime).Value = datehingga;
            //cmd.Parameters.Add("@kelompok", SqlDbType.VarChar).Value = kelompok_name;
            //cmd.Parameters.Add("@Dividen ", SqlDbType.VarChar).Value = Peratusan_dividen;
            //SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //adp.Fill(dt);
            //DataSet ds = new DataSet();
            //adp.Fill(ds, "mda_procedure");
            //foreach (DataRow theRow in ds.Tables["mda_procedure"].Rows)
            //{
              
            //    DataTable ddlvl1 = new DataTable();
            //    ddlvl1 = Con.Ora_Execute_table(" select div_new_icno from mem_divident where div_new_icno='" + theRow["mem_new_icno"] + "' and Acc_sts ='Y' and div_process_dt='" + DateTime.Now.ToString("yyyy/MM/dd") + "'");
                
            //    string strDate = DateTime.Now.ToString("yyyy-MM-dd");

            //    int year = Convert.ToDateTime(datehingga).Year;
               
            //    if (ddlvl1.Rows.Count == 0)
            //    {
            //        SqlCommand cmd1 = new SqlCommand("insert into mem_divident (div_new_icno,div_process_dt,div_remark,div_bank_acc_no,div_bank_cd,div_debit_amt,div_batch_name,div_start_dt,div_pay_dt,div_end_dt,Acc_sts,div_crt_id,div_crt_dt) values (@div_new_icno,@div_process_dt,@div_remark,@div_bank_acc_no,@div_bank_cd,@div_debit_amt,@div_batch_name,@div_start_dt,@div_pay_dt,@div_end_dt,@Acc_sts,@div_crt_id,@div_crt_dt)", con);
            //        cmd1.Parameters.AddWithValue("div_new_icno", theRow["mem_new_icno"]);
            //        cmd1.Parameters.AddWithValue("div_process_dt", strDate);
            //        cmd1.Parameters.AddWithValue("div_remark", remark.Value);
            //        cmd1.Parameters.AddWithValue("div_bank_acc_no", theRow["mem_bank_acc_no"]);
            //        cmd1.Parameters.AddWithValue("div_bank_cd", theRow["mem_bank_cd"].ToString());
            //        cmd1.Parameters.AddWithValue("div_debit_amt", theRow["sha_credit_amt"]);
            //        cmd1.Parameters.AddWithValue("div_batch_name", kelompok.Text);
            //        cmd1.Parameters.AddWithValue("div_start_dt", datedari);
            //        cmd1.Parameters.AddWithValue("div_pay_dt", "");
            //        cmd1.Parameters.AddWithValue("div_end_dt", datehingga);
            //        cmd1.Parameters.AddWithValue("Acc_sts", "Y");
            //        cmd1.Parameters.AddWithValue("div_crt_id", Session["New"].ToString());
            //        cmd1.Parameters.AddWithValue("div_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //        con.Open();
            //        int j = cmd1.ExecuteNonQuery();
            //        con.Close();
            //    }
               

            //}

        }
        return dt;
    }


    public void ShowReport()
    {
        try
        {
            string fmdate = fromdate.Text;
            DateTime ft = DateTime.ParseExact(fmdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datedari = ft.ToString("yyyy-mm-dd");

            string tdate = todate.Text;
            DateTime td = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datehingga = td.ToString("yyyy-mm-dd");

            string sanggota = s_anggota.SelectedItem.Value;

            string kelompok_name = kelompok.Text;

            string Peratusan_dividen = TextBox1.Text;

            DateTime dmula;
            DateTime dakhir;
            DateTime today = DateTime.Now;
            //DataSource
            DataTable dt = new DataTable();

            dmula = today;
            dakhir = today;



            //dt = GetData(datedari, datehingga, sanggota, kelompok_name, Peratusan_dividen);

            dt = DBCon.Ora_Execute_table("select distinct mem_new_icno,mem_name,mem_bank_acc_no,mem_member_no,mem_region_cd,mm.mem_bank_cd,mem_centre,(sum(sha_debit_amt) - sum(sha_credit_amt)) as jumlah,sum(sha_balance_amt) as sha_balance_amt,(((sum(sha_debit_amt) - sum(sha_credit_amt))  * '" + Peratusan_dividen + "') / 100) as sha_credit_amt,rb.branch_desc as Cawangan_name,rn.Bank_Name,rw.Wilayah_Name from mem_member  AS mm Left join  mem_share AS ms ON mm.mem_new_icno = ms.sha_new_icno and ms.Acc_sts ='Y' left join Ref_Wilayah rw on rw.Wilayah_Code=mm.mem_region_cd left join ref_branch rb on rb.branch_cd=mm.mem_branch_cd left join Ref_Nama_Bank rn on mm.mem_bank_cd=rn.Bank_Code where ms.Acc_sts ='Y' and ms.sha_approve_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + datedari + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + datehingga + "'), +1)  and mm.mem_sts_cd='SA' and ms.sha_approve_sts_cd IN ('S','SA')  group by mem_new_icno,mem_name,mem_member_no,mem_region_cd,mem_centre,mem_bank_acc_no,mem_bank_cd,rb.branch_desc,rw.Wilayah_Name,rn.Bank_Name Order by rw.Wilayah_Name,Cawangan_name,mem_centre,mem_new_icno");            

            //DataTable over_rcds = new DataTable();
            //over_rcds = Con.Ora_Execute_table(" select distinct mem_new_icno,mem_name,mem_bank_acc_no,mem_member_no,mem_region_cd,mm.mem_bank_cd,mem_centre,(sum(sha_debit_amt) - sum(sha_credit_amt)) as jumlah,sum(sha_balance_amt) as sha_balance_amt,(((sum(sha_debit_amt) - sum(sha_credit_amt))  * '" + Peratusan_dividen + "') / 100) as sha_credit_amt,rb.branch_desc as Cawangan_name,rn.Bank_Name,rw.Wilayah_Name from mem_member  AS mm Left join  mem_share AS ms ON mm.mem_new_icno = ms.sha_new_icno and ms.Acc_sts ='Y' left join Ref_Wilayah rw on rw.Wilayah_Code=mm.mem_region_cd left join ref_branch rb on rb.branch_cd=mm.mem_branch_cd left join Ref_Nama_Bank rn on mm.mem_bank_cd=rn.Bank_Code where ms.Acc_sts ='Y' and ms.sha_approve_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + datedari + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + datehingga + "'), +1)  and mm.mem_sts_cd='SA' and ms.sha_approve_sts_cd IN ('S','SA')  group by mem_new_icno,mem_name,mem_member_no,mem_region_cd,mem_centre,mem_bank_acc_no,mem_bank_cd,rb.branch_desc,rw.Wilayah_Name,rn.Bank_Name Order by rw.Wilayah_Name,Cawangan_name,mem_centre,mem_new_icno");

            foreach (DataRow theRow in dt.Rows)
            {
                //string customerTable = theRow["sha__amt"].ToString();
                //string txt = (double.Parse(customerTable) * double.Parse(TextBox1.Text)).ToString();
                //string val1 = (double.Parse(txt) / 100).ToString();

                DataTable ddlvl1 = new DataTable();
                ddlvl1 = Con.Ora_Execute_table(" select div_new_icno from mem_divident where div_new_icno='" + theRow["mem_new_icno"] + "' and Acc_sts ='Y' and div_process_dt='" + DateTime.Now.ToString("yyyy/MM/dd") + "'");
                //for (i = 0; i < ddlvl1.Rows.Count; i++)
                //{
                string strDate = DateTime.Now.ToString("yyyy-MM-dd");

                int year = Convert.ToDateTime(datehingga).Year;
                //remark.Value = "DIVIDEN BAGI TAHUN " + year;


                //int debit_amt = Convert.ToInt32(theRow["sha_debit_amt"]) * Convert.ToInt32(TextBox1.Text);
                //int debit_amt = Convert.ToInt16(theRow["sha_debit_amt"]) * Convert.ToInt16(TextBox1.Text);
                if (ddlvl1.Rows.Count == 0)
                {
                    SqlCommand cmd1 = new SqlCommand("insert into mem_divident (div_new_icno,div_process_dt,div_remark,div_bank_acc_no,div_bank_cd,div_debit_amt,div_batch_name,div_start_dt,div_pay_dt,div_end_dt,Acc_sts,div_crt_id,div_crt_dt) values (@div_new_icno,@div_process_dt,@div_remark,@div_bank_acc_no,@div_bank_cd,@div_debit_amt,@div_batch_name,@div_start_dt,@div_pay_dt,@div_end_dt,@Acc_sts,@div_crt_id,@div_crt_dt)", con);
                    cmd1.Parameters.AddWithValue("div_new_icno", theRow["mem_new_icno"]);
                    cmd1.Parameters.AddWithValue("div_process_dt", strDate);
                    cmd1.Parameters.AddWithValue("div_remark", remark.Value);
                    cmd1.Parameters.AddWithValue("div_bank_acc_no", theRow["mem_bank_acc_no"]);
                    cmd1.Parameters.AddWithValue("div_bank_cd", theRow["mem_bank_cd"].ToString());
                    cmd1.Parameters.AddWithValue("div_debit_amt", theRow["sha_credit_amt"]);
                    cmd1.Parameters.AddWithValue("div_batch_name", kelompok.Text);
                    cmd1.Parameters.AddWithValue("div_start_dt", datedari);
                    cmd1.Parameters.AddWithValue("div_pay_dt", "");
                    cmd1.Parameters.AddWithValue("div_end_dt", datehingga);
                    cmd1.Parameters.AddWithValue("Acc_sts", "Y");
                    cmd1.Parameters.AddWithValue("div_crt_id", Session["New"].ToString());
                    cmd1.Parameters.AddWithValue("div_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    con.Open();
                    int j = cmd1.ExecuteNonQuery();
                    con.Close();
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Rekod Telah Dijana.');", true);
                //}

            }

            //Reset
            RptviwerStudent.Reset();

            List<DataRow> listResult = dt.AsEnumerable().ToList();
            listResult.Count();
            int countRow = 0;
            countRow = listResult.Count();

            if (countRow != 0)
            {
                //shw_txt1.Visible = true;
                // Label1.Text = "";
                //Label2.Text = "";
                ReportDataSource rds = new ReportDataSource("DataSet1", dt);

                RptviwerStudent.LocalReport.DataSources.Add(rds);

                //Path
                RptviwerStudent.LocalReport.ReportPath = "keanggotan/mda_anggota.rdlc";
                //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                //Parameters
                ReportParameter[] rptParams = new ReportParameter[]{
                    new ReportParameter("fromDate", fromdate.Text),
                     new ReportParameter("toDate", todate.Text),
                     new ReportParameter("Kelompok", kelompok.Text),
                     new ReportParameter("Anggota", s_anggota.SelectedItem.Text),
                     new ReportParameter("Dividen", TextBox1.Text),
                     new ReportParameter("current_date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                     };


                RptviwerStudent.LocalReport.SetParameters(rptParams);

                //Refresh
                RptviwerStudent.LocalReport.Refresh();
                System.Threading.Thread.Sleep(1);

            }
            else if (countRow == 0)
            {
                //Label1.Text = "Maklumat Carian Tidak Dijumpai";
                
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Tiada Rekod Dijumpai Dalam Julat Tarikh Yang Dimasukkan.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
            }

        }

        catch (Exception ex)
        {
            //Label2.Text = ex.ToString();
        }
    }

    public void ShowReport_test()
    {
        try
        {
           

            string script1 = "$(function () { $('[id*=GridView1]').prepend($('<thead></thead>').append($(this).find('tr:first'))).DataTable({ 'responsive': true, 'sPaginationType': 'full_numbers'   }); });";


            ScriptManager.RegisterStartupScript(this, this.GetType(), "", script1, true);



            string fmdate = fromdate.Text;
            DateTime ft = DateTime.ParseExact(fmdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datedari = ft.ToString("yyyy-mm-dd");

            string tdate = todate.Text;
            DateTime td = DateTime.ParseExact(tdate, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            String datehingga = td.ToString("yyyy-mm-dd");

            string sanggota = s_anggota.SelectedItem.Value;

            string kelompok_name = kelompok.Text;

            string Peratusan_dividen = TextBox1.Text;

            DateTime dmula;
            DateTime dakhir;
            DateTime today = DateTime.Now;
            //DataSource
            DataTable dt = new DataTable();

            dmula = today;
            dakhir = today;



            //dt = GetData(datedari, datehingga, sanggota, kelompok_name, Peratusan_dividen);
            dt = DBCon.Ora_Execute_table("select distinct mem_new_icno,mem_name,mem_bank_acc_no,mem_member_no,mem_region_cd,mm.mem_bank_cd,mem_centre,(sum(ISNULL(sha_debit_amt,'0.00')) - sum(ISNULL(sha_credit_amt,'0.00'))) as jumlah,sum(ISNULL(sha_balance_amt,'0.00')) as sha_balance_amt,(((sum(ISNULL(sha_debit_amt,'0.00')) - sum(ISNULL(sha_credit_amt,'0.00')))  * '" + Peratusan_dividen + "') / 100) as sha_credit_amt,rb.branch_desc as Cawangan_name,rn.Bank_Name,rw.Wilayah_Name from mem_member  AS mm Left join  mem_share AS ms ON mm.mem_new_icno = ms.sha_new_icno and ms.Acc_sts ='Y' left join Ref_Wilayah rw on rw.Wilayah_Code=mm.mem_region_cd left join ref_branch rb on rb.branch_cd=mm.mem_branch_cd left join Ref_Nama_Bank rn on mm.mem_bank_cd=rn.Bank_Code where ms.Acc_sts ='Y' and ms.sha_approve_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + datedari + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + datehingga + "'), +1)  and mm.mem_sts_cd='SA' and ms.sha_approve_sts_cd IN ('S','SA')  group by mem_new_icno,mem_name,mem_member_no,mem_region_cd,mem_centre,mem_bank_acc_no,mem_bank_cd,rb.branch_desc,rw.Wilayah_Name,rn.Bank_Name Order by rw.Wilayah_Name,Cawangan_name,mem_centre,mem_new_icno");

            DataTable dt3 = new DataTable();
            dt3 = Con.Ora_Execute_table("select count(*) mem_member from (select  count(mem_new_icno) mem_new_icno from mem_member  AS mm Left join  mem_share AS ms ON mm.mem_new_icno = ms.sha_new_icno and ms.Acc_sts ='Y' left join Ref_Wilayah rw on rw.Wilayah_Code=mm.mem_region_cd left join ref_branch rb on rb.branch_cd=mm.mem_branch_cd left join Ref_Nama_Bank rn on mm.mem_bank_cd=rn.Bank_Code where ms.Acc_sts ='Y' and ms.sha_approve_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + datedari + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + datehingga + "'), +1)  and mm.mem_sts_cd='SA' and ms.sha_approve_sts_cd IN ('S','SA')  group by mem_new_icno) a ");
            if (dt3.Rows.Count > 0)
            {
                Label3.Text = Peratusan_dividen;
                Label12.Text = remark.Value;
                Label4.Text = dt3.Rows[0][0].ToString();
            }
            DataTable dt4 = new DataTable();
            dt4 = Con.Ora_Execute_table("select (sum(ISNULL(sha_debit_amt,'0.00')) - sum(ISNULL(sha_credit_amt,'0.00'))) as jumlah,(((sum(ISNULL(sha_debit_amt,'0.00')) - sum(ISNULL(sha_credit_amt,'0.00')))  * '" + TextBox1.Text + "') / 100) as sha_credit_amt from mem_member  AS mm Left join  mem_share AS ms ON mm.mem_new_icno = ms.sha_new_icno and ms.Acc_sts ='Y' left join Ref_Wilayah rw on rw.Wilayah_Code=mm.mem_region_cd left join ref_branch rb on rb.branch_cd=mm.mem_branch_cd left join Ref_Nama_Bank rn on mm.mem_bank_cd=rn.Bank_Code where ms.Acc_sts ='Y' and ms.sha_approve_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + datedari + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + datehingga + "'), +1)  and mm.mem_sts_cd='SA' and ms.sha_approve_sts_cd IN ('S','SA') ");
            if (dt4.Rows.Count > 0)
            {
                decimal s1 = Convert.ToDecimal(dt4.Rows[0][0].ToString());
                decimal s2 = Convert.ToDecimal(dt4.Rows[0][1].ToString());
                Label5.Text = "RM " + String.Format("{0:N2}", s1);
                Label6.Text = "RM " + String.Format("{0:N2}", s2);
            }

            //DataTable over_rcds = new DataTable();
            //over_rcds = Con.Ora_Execute_table(" select distinct mem_new_icno,mem_name,mem_bank_acc_no,mem_member_no,mem_region_cd,mm.mem_bank_cd,mem_centre,(sum(sha_debit_amt) - sum(sha_credit_amt)) as jumlah,sum(sha_balance_amt) as sha_balance_amt,(((sum(sha_debit_amt) - sum(sha_credit_amt))  * '" + Peratusan_dividen + "') / 100) as sha_credit_amt,rb.branch_desc as Cawangan_name,rn.Bank_Name,rw.Wilayah_Name from mem_member  AS mm Left join  mem_share AS ms ON mm.mem_new_icno = ms.sha_new_icno and ms.Acc_sts ='Y' left join Ref_Wilayah rw on rw.Wilayah_Code=mm.mem_region_cd left join ref_branch rb on rb.branch_cd=mm.mem_branch_cd left join Ref_Nama_Bank rn on mm.mem_bank_cd=rn.Bank_Code where ms.Acc_sts ='Y' and ms.sha_approve_dt BETWEEN DATEADD(day, DATEDIFF(day, 0, '" + datedari + "'), 0) AND DATEADD(day, DATEDIFF(day, 0, '" + datehingga + "'), +1)  and mm.mem_sts_cd='SA' and ms.sha_approve_sts_cd IN ('S','SA')  group by mem_new_icno,mem_name,mem_member_no,mem_region_cd,mem_centre,mem_bank_acc_no,mem_bank_cd,rb.branch_desc,rw.Wilayah_Name,rn.Bank_Name Order by rw.Wilayah_Name,Cawangan_name,mem_centre,mem_new_icno");

            foreach (DataRow theRow in dt.Rows)
            {
                //string customerTable = theRow["sha__amt"].ToString();
                //string txt = (double.Parse(customerTable) * double.Parse(TextBox1.Text)).ToString();
                //string val1 = (double.Parse(txt) / 100).ToString();

                DataTable ddlvl1 = new DataTable();
                ddlvl1 = Con.Ora_Execute_table(" select div_new_icno from mem_divident where div_new_icno='" + theRow["mem_new_icno"] + "' and Acc_sts ='Y' and div_process_dt='" + DateTime.Now.ToString("yyyy/MM/dd") + "'");
                //for (i = 0; i < ddlvl1.Rows.Count; i++)
                //{
                string strDate = DateTime.Now.ToString("yyyy-MM-dd");

                int year = Convert.ToDateTime(datehingga).Year;
                //remark.Value = "DIVIDEN BAGI TAHUN " + year;


                //int debit_amt = Convert.ToInt32(theRow["sha_debit_amt"]) * Convert.ToInt32(TextBox1.Text);
                //int debit_amt = Convert.ToInt16(theRow["sha_debit_amt"]) * Convert.ToInt16(TextBox1.Text);
                if (ddlvl1.Rows.Count == 0)
                {
                    SqlCommand cmd1 = new SqlCommand("insert into mem_divident (div_new_icno,div_process_dt,div_remark,div_bank_acc_no,div_bank_cd,div_debit_amt,div_batch_name,div_start_dt,div_pay_dt,div_end_dt,Acc_sts,div_crt_id,div_crt_dt,sha_amt) values (@div_new_icno,@div_process_dt,@div_remark,@div_bank_acc_no,@div_bank_cd,@div_debit_amt,@div_batch_name,@div_start_dt,@div_pay_dt,@div_end_dt,@Acc_sts,@div_crt_id,@div_crt_dt,@sha_amt)", con);
                    cmd1.Parameters.AddWithValue("div_new_icno", theRow["mem_new_icno"]);
                    cmd1.Parameters.AddWithValue("div_process_dt", strDate);
                    cmd1.Parameters.AddWithValue("div_remark", remark.Value);
                    cmd1.Parameters.AddWithValue("div_bank_acc_no", theRow["mem_bank_acc_no"]);
                    cmd1.Parameters.AddWithValue("div_bank_cd", theRow["mem_bank_cd"].ToString());
                    cmd1.Parameters.AddWithValue("div_debit_amt", theRow["sha_credit_amt"]);
                    cmd1.Parameters.AddWithValue("div_batch_name", kelompok.Text);
                    cmd1.Parameters.AddWithValue("div_start_dt", datedari);
                    cmd1.Parameters.AddWithValue("div_pay_dt", "");
                    cmd1.Parameters.AddWithValue("div_end_dt", datehingga);
                    cmd1.Parameters.AddWithValue("Acc_sts", "Y");
                    cmd1.Parameters.AddWithValue("div_crt_id", Session["New"].ToString());
                    cmd1.Parameters.AddWithValue("div_crt_dt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd1.Parameters.AddWithValue("sha_amt", theRow["jumlah"].ToString());
                    con.Open();
                    int j = cmd1.ExecuteNonQuery();
                    con.Close();
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Rekod Telah Dijana.');", true);
                //}

            }
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                GridView1.DataSource = dt;
                GridView1.DataBind();
                int columncount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView1.Rows[0].Cells[0].Text = "<center><strong>Maklumat Carian Tidak Dijumpai</strong></center>";
                d1.Visible = false;
                d2.Visible = false;
                d3.Visible = false;
                d4.Visible = false;
                d5.Visible = false;
            }
            else
            {
                service.audit_trail("P0137", remark.Value, "Nama Kelompok", kelompok.Text);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                d1.Visible = true;
                d2.Visible = true;
                d3.Visible = true;
                d4.Visible = true;
                d5.Visible = true;
            }


        }

        catch (Exception ex)
        {
            //Label2.Text = ex.ToString();
        }
    }

    protected void Reset_btn(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}