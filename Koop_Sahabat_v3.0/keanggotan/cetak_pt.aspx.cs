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

public partial class cetak_pt : System.Web.UI.Page
{

    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);
    DataTable wilayah = new DataTable();
    DataTable caw = new DataTable();
    DataTable pusat = new DataTable();
    DBConnection DBCon = new DBConnection();
    DataTable ddcom = new DataTable();
    string Status = string.Empty;
    string level, userid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {

                //Txtnama.Attributes.Add("readonly", "readonly");
                //Textarea1.Attributes.Add("readonly", "readonly");
                //TextBox3.Attributes.Add("readonly", "readonly");
                //ddlnegri.Attributes.Add("readonly", "readonly");
                //Txtnokp.Attributes.Add("readonly", "readonly");
                //DD_bangsa.Attributes.Add("readonly", "readonly");
                //DD_pemohan.Attributes.Add("readonly", "readonly");
                //DD_cawangan.Attributes.Add("readonly", "readonly");
                //DD_wilayah.Attributes.Add("readonly", "readonly");
                //TextBox2.Attributes.Add("readonly", "readonly");
                //TextBox1.Attributes.Add("readonly", "readonly");
                //DD_jantina.Attributes.Add("readonly", "readonly");


                level = Session["level"].ToString();
                userid = Session["New"].ToString();
                //DropDownList4.SelectedValue = DateTime.Now.ToString("yyyy");
                Year();
                cBind();
                wBind();
                BangsaBind();
                PemohanBind();
                JantinaBind();
                negriBind();
                search();
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }

    private void Year()
    {
        int yr = 0;
        DataTable regyear = new DataTable();
        regyear = DBCon.Ora_Execute_table("select year(mem_register_dt) reg_year from mem_member where mem_new_icno='"+ Session["userid"].ToString() + "'");

        if(regyear.Rows.Count != 0)
        {
            yr = (Int32.Parse(DateTime.Now.Year.ToString()) - Int32.Parse(regyear.Rows[0]["reg_year"].ToString()));
        }

        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
        int year = DateTime.Now.Year - yr;
        for (int Y = year; Y <= DateTime.Now.Year; Y++)
        {
            DropDownList4.Items.Add(new ListItem(Y.ToString(), Y.ToString()));
        }
        DropDownList4.SelectedValue = DateTime.Now.Year.ToString();

    }
    void cBind()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select branch_cd,branch_desc from Ref_branch order by branch_desc ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_cawangan.DataSource = dt;
            DD_cawangan.DataBind();
            DD_cawangan.DataTextField = "branch_desc";
            DD_cawangan.DataValueField = "branch_cd";
            DD_cawangan.DataBind();
            DD_cawangan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void wBind()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select Wilayah_Code,Wilayah_Name from ref_wilayah order by Wilayah_Name ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_wilayah.DataSource = dt;
            DD_wilayah.DataBind();
            DD_wilayah.DataTextField = "Wilayah_Name";
            DD_wilayah.DataValueField = "Wilayah_Code";
            DD_wilayah.DataBind();
            DD_wilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void BangsaBind()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select * from Ref_Bangsa order by Bangsa_Name ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_bangsa.DataSource = dt;
            DD_bangsa.DataBind();
            DD_bangsa.DataTextField = "Bangsa_Name";
            DD_bangsa.DataValueField = "Bangsa_Code";
            DD_bangsa.DataBind();
            DD_bangsa.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void PemohanBind()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select * from Ref_Applicant_Category order by Applicant_Name ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_pemohan.DataSource = dt;
            DD_pemohan.DataBind();
            DD_pemohan.DataTextField = "Applicant_Name";
            DD_pemohan.DataValueField = "Applicant_Code";
            DD_pemohan.DataBind();
            DD_pemohan.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void JantinaBind()
    {
        DataSet Ds = new DataSet();
        try
        {

            SqlConnection con = new SqlConnection(conString);
            string com = "select * from ref_gender order by gender_desc ASC";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            DD_jantina.DataSource = dt;
            DD_jantina.DataBind();
            DD_jantina.DataTextField = "gender_desc";
            DD_jantina.DataValueField = "gender_cd";
            DD_jantina.DataBind();
            DD_jantina.Items.Insert(0, new ListItem("--- PILIH ---", ""));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void ddkaw_SelectedIndexChanged(object sender, EventArgs e)
    {

        //-Pusat---------------------------------------------------------------------------------
        string cmd6 = "select distinct wilayah_code,wilayah_name from  Ref_Cawangan where cawangan_code='" + DD_cawangan.SelectedItem.Value + "'";
        con.Open();
        SqlDataAdapter adapterP = new SqlDataAdapter(cmd6, con);
        adapterP.Fill(wilayah);

        DD_wilayah.DataSource = wilayah;
        DD_wilayah.DataTextField = "wilayah_name";
        DD_wilayah.DataValueField = "wilayah_code";
        DD_wilayah.DataBind();
        //ddPusat.Items.RemoveAt(0); //remove 'Semua Wilayah'
        con.Close();

        DD_wilayah.Items.Insert(0, new ListItem("--- PILIH ---", ""));

    }


    void negriBind()
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection con = new SqlConnection(conString);
            string com = "select Decription,Decription_Code from Ref_Negeri  group by Decription,Decription_Code order by Decription,Decription_Code";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlnegri.DataSource = dt;
            ddlnegri.DataBind();
            ddlnegri.DataTextField = "Decription";
            ddlnegri.DataValueField = "Decription_Code";
            ddlnegri.DataBind();
            ddlnegri.Items.Insert(0, new ListItem("--- PILIH ---", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

     void search()
    {

        string query = "select *,ISNULL(CASE WHEN CONVERT(DATE, fee_approval_dt) = '1900-01-01' THEN '' ELSE CONVERT(CHAR(10), fee_approval_dt, 103) END, '') AS fee_approval_dt, ISNULL(mem_postcd, '') as mempospcd,ISNULL(mem_negri, '') as memnegri,case(mem_phone_o) when 'NULL' then'' else mem_phone_o end as memphoneo,case(mem_phone_h) when 'NULL' then'' else mem_phone_h end as memphoneh,case(mem_phone_m) when 'NULL' then'' else mem_phone_m end as memphonem from mem_member as mm Left join mem_fee AS mf ON mf.fee_new_icno = mm.mem_new_icno and mf.Acc_sts ='Y' Left join  Ref_Wilayah AS rw ON mm.mem_region_cd = rw.Wilayah_Code Left join Ref_Cawangan AS rc ON mm.mem_branch_cd = rc.cawangan_Code and mm.mem_area_cd=rc.kawasan_code left join mem_share ms on ms.sha_new_icno=mm.mem_new_icno and ms.Acc_sts ='Y' where mem_new_icno='" + userid + "' and mm.Acc_sts ='Y'";
        con.Open();
        var sqlCommand = new SqlCommand(query, con);
        var sqlReader = sqlCommand.ExecuteReader();

        if (sqlReader.Read() == true)
        {
            Txtnama.Text = sqlReader["mem_name"].ToString();
            Textarea1.Value = sqlReader["mem_address"].ToString();
            TextBox3.Text = sqlReader["mempospcd"].ToString();
            ddlnegri.SelectedValue = sqlReader["memnegri"].ToString();

            Txtnokp.Text = sqlReader["mem_new_icno"].ToString();
            DD_bangsa.SelectedValue = sqlReader["mem_race_cd"].ToString();
            DD_pemohan.Text = sqlReader["mem_sts_cd"].ToString();
            DD_cawangan.SelectedValue = sqlReader["mem_branch_cd"].ToString();
            DD_wilayah.SelectedValue = sqlReader["mem_region_cd"].ToString();
            TextBox2.Text = sqlReader["mem_centre"].ToString();
            TextBox1.Text = sqlReader["mem_member_no"].ToString();
            DD_jantina.SelectedValue = sqlReader["mem_gender_cd"].ToString();

        }
        else
        {
            btnsave.Visible = false;
        }
           
         sqlReader.Close();
    }

     protected void Click_print(object sender, EventArgs e)
     {
         try
         {
            string ss1 = string.Empty, ss2 = string.Empty, ss3 = string.Empty, ss4 = string.Empty;
            if (Txtnokp.Text != "")
             {
                 if (DropDownList4.SelectedValue != "")
                 {
                     //Path
                     DataSet ds = new DataSet();
                     DataTable dt = new DataTable();
                     //dt = DBCon.Ora_Execute_table("SELECT DISTINCT  A.mem_member_no, A.mem_name, A.mem_address, A.mem_new_icno, A.mem_phone_m, A.gender_desc, A.Bangsa_Name, A.Wilayah_Name, A.mem_centre, A.cawangan_name,  A.mem_fee_amount, CONVERT(VARCHAR(10),GETDATE(),105) as cdate, B.ftunai,B.fpst,B.SPST,B.STUNAI,B.jumlah,c.sha_approve_Dt, C.sha_item,C. sha_reference_ind,C. sha_debit_amt,C. sha_credit_amt,c.Jumla,d.div_pay_dt,d.div_remark,d.Bank_Name as bname,d.div_bank_acc_no,d.div_debit_amt,a.Applicant_Name,e.ast_end_date,e.ast_monthly_collect_amt,e.ast_st_balance_amt FROM ((select mem_member_no,mem_name,mem_address,mem_new_icno,mem_phone_m,rg.gender_desc,rb.Bangsa_Name,rw.Wilayah_Name,mm.mem_centre,mem_fee_amount,ra.Applicant_Name,br.branch_desc as cawangan_name  from mem_member as mm Left join  Ref_Wilayah AS rw ON mm.mem_region_cd = rw.Wilayah_Code Left join Ref_Cawangan AS rc ON mm.mem_area_cd=rc.kawasan_code inner join Ref_Bangsa rb on rb.Bangsa_Code=mm.mem_race_cd inner join ref_gender rg on rg.gender_cd=mm.mem_gender_cd  inner join Ref_Applicant_Category ra on ra.Applicant_Code=mm.mem_applicant_type_cd left join ref_branch br on br.branch_cd=mm.mem_branch_cd  ) a FULL OUTER JOIN (select b.FTUNAI,b.FPST,a.STUNAI,a.SPST, a.STUNAI + a.SPST as Jumlah,a.sha_new_icno  from (select * from (select isnull([STUNAI],'') as STUNAI,isnull([SPST],'') as SPST,sha_new_icno from (select  SUM(sha_debit_amt) - sum(sha_credit_amt) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + Txtnokp.Text + "' and sha_refund_ind='N' and year(sha_approve_dt) = '" + DropDownList4.SelectedValue + "'  group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final )a full outer join (select * from (select isnull([FTUNAI],'') as FTUNAI,isnull([FPST],'') as FPST,fee_new_icno from (select  SUM(fee_amount) as Tran_count, case (fee_payment_type_cd) WHEN 'C' THEN 'FTUNAI' WHEN 'P' THEN 'FPST' END MONTHNAME,fee_new_icno from mem_fee where fee_new_icno='860715235832' and fee_refund_ind='N' group by fee_payment_type_cd,fee_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([FTUNAI], [FPST]))AS PivotTable) as final )b on b.fee_new_icno=a.sha_new_icno)b  on A.mem_new_icno= '" + Txtnokp.Text + "' FULL OUTER JOIN  (select ISNULL(CASE WHEN sha_approve_Dt = '1900-01-01 00:00:00.000' THEN '' ELSE sha_approve_Dt END, '') AS sha_approve_Dt, UPPER(sha_item) as sha_item,case(sha_reference_ind) when 'C' then 'TUNAI'  when 'P' then 'PST' end as sha_reference_ind,sha_debit_amt,sha_credit_amt,Jumla=(sum(sha_debit_amt)-Sum(sha_credit_amt)),ms.sha_new_icno from mem_member AS mm left join Ref_Nama_Bank as bn ON mm.mem_bank_cd=bn.Bank_Code Left join mem_share AS ms ON ms.sha_new_icno = mm.mem_new_icno and ms.sha_refund_ind='N' and year(sha_approve_dt) = '" + DropDownList4.SelectedValue + "' group by sha_approve_Dt,sha_item,sha_reference_ind ,sha_debit_amt,sha_credit_amt,sha_new_icno  )c on c.sha_new_icno=a.mem_new_icno FULL OUTER JOIN (select  Convert(CHAR(10), div_pay_dt, 105) as div_pay_dt,div_remark,Bank_Name,div_bank_acc_no,div_debit_amt,div_new_icno from mem_member AS mm left join Ref_Nama_Bank as bn ON mm.mem_bank_cd=bn.Bank_Code Left join mem_divident AS md ON md.div_new_icno = mm.mem_new_icno and year(md.div_pay_dt)='" + DropDownList4.SelectedValue + "') d on d.div_new_icno=a.mem_new_icno FULL OUTER JOIN (select Convert(char(10),ast_end_date,105) as ast_end_date,ast_st_balance_amt,ast_monthly_collect_amt,ast_new_icno from aim_st ) e on e.ast_new_icno=a.mem_new_icno) where a.mem_new_icno='" + Txtnokp.Text + "'");
                     dt = DBCon.Ora_Execute_table("SELECT DISTINCT  Format(fee_approval_dt, 'dd/MM/yyyy') fee_approval_dt,A.mem_member_no, A.mem_name, A.mem_address, A.mem_new_icno, ISNULL(CASE WHEN A.mem_phone_m = 'NULL' THEN '' else A.mem_phone_m END, '') as mem_phone_m, A.gender_desc, A.Bangsa_Name, A.Wilayah_Name, A.mem_centre, A.cawangan_name,  A.mem_fee_amount, CONVERT(VARCHAR(10),GETDATE(),105) as cdate, B.ftunai,B.fpst,B.SPST,B.STUNAI,B.jumlah, Convert(CHAR(10),c.sha_txn_dt,105) as sha_approve_Dt, C.sha_item,C. sha_reference_ind,C. sha_debit_amt,C. sha_credit_amt,c.Jumla,d.div_pay_dt,d.div_remark,d.Bank_Name as bname,d.div_bank_acc_no,d.div_debit_amt,a.Applicant_Name,e.ast_end_date,e.ast_monthly_collect_amt,e.ast_st_balance_amt FROM ((select mem_member_no,mem_name,mem_address,mem_new_icno,mem_phone_m,rg.gender_desc,rb.Bangsa_Name,rw.Wilayah_Name,mm.mem_centre,mem_fee_amount,ra.Applicant_Name,br.branch_desc as cawangan_name  from mem_member as mm Left join  Ref_Wilayah AS rw ON mm.mem_region_cd = rw.Wilayah_Code Left join Ref_Cawangan AS rc ON mm.mem_area_cd=rc.kawasan_code left join Ref_Bangsa rb on rb.Bangsa_Code=mm.mem_race_cd left join ref_gender rg on rg.gender_cd=mm.mem_gender_cd  Left join Ref_Applicant_Category ra on ra.Applicant_Code=mm.mem_applicant_type_cd left join ref_branch br on br.branch_cd=mm.mem_branch_cd  where mm.Acc_sts='Y') a FULL OUTER JOIN (select b.FTUNAI,b.FPST,a.STUNAI,a.SPST, a.STUNAI + a.SPST as Jumlah,a.sha_new_icno,fee_approval_dt  from (select * from (select isnull([STUNAI],'0.00') as STUNAI,isnull([SPST],'0.00') as SPST,sha_new_icno from (select  SUM(ISNULL(sha_debit_amt,'0.00')) - sum(ISNULL(sha_credit_amt,'0.00')) as Tran_count, case (sha_reference_ind) WHEN 'C' THEN 'STUNAI' WHEN 'P' THEN 'SPST' END MONTHNAME,sha_new_icno from mem_share where sha_new_icno='" + Txtnokp.Text + "' and Acc_sts ='Y' and sha_refund_ind='N'   group by sha_reference_ind,sha_new_icno ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([STUNAI], [SPST]))AS PivotTable) as final )a full outer join (select * from (select isnull([FTUNAI],'') as FTUNAI,isnull([FPST],'') as FPST,fee_new_icno,fee_approval_dt from (select  SUM(fee_amount) as Tran_count, case (fee_payment_type_cd) WHEN 'C' THEN 'FTUNAI' WHEN 'P' THEN 'FPST' END MONTHNAME,fee_new_icno,fee_approval_dt from mem_fee where fee_new_icno='" + Txtnokp.Text + "' and Acc_sts ='Y' and fee_refund_ind='N'  group by fee_payment_type_cd,fee_new_icno,fee_approval_dt ) as Games PIVOT(MIN(Tran_count) FOR MONTHNAME in ([FTUNAI], [FPST]))AS PivotTable) as final )b on b.fee_new_icno=a.sha_new_icno)b  on A.mem_new_icno= '" + Txtnokp.Text + "' FULL OUTER JOIN  (select ISNULL(CASE WHEN sha_txn_dt = '1900-01-01 00:00:00.000' THEN '' ELSE sha_txn_dt END, '') AS sha_txn_dt, UPPER(sha_item) as sha_item,case(sha_reference_ind) when 'C' then 'TUNAI'  when 'P' then 'PST' end as sha_reference_ind,ISNULL(sha_debit_amt,'0.00')sha_debit_amt,ISNULL(sha_credit_amt,'0.00') sha_credit_amt,Jumla=(sum(ISNULL(sha_debit_amt,'0.00'))-Sum(ISNULL(sha_credit_amt,'0.00'))),ms.sha_new_icno from mem_member AS mm left join Ref_Nama_Bank as bn ON mm.mem_bank_cd=bn.Bank_Code Left join mem_share AS ms ON ms.sha_new_icno = mm.mem_new_icno and ms.Acc_sts ='Y' where mm.Acc_sts ='Y' and ms.sha_refund_ind='N' and year(sha_txn_dt) <= '" + DropDownList4.SelectedItem.Text + "' and ms.sha_new_icno='" + Txtnokp.Text + "'  group by sha_txn_dt,sha_item,sha_reference_ind ,sha_debit_amt,sha_credit_amt,sha_new_icno  )c on c.sha_new_icno=a.mem_new_icno FULL OUTER JOIN (select  Convert(CHAR(10), div_pay_dt, 105) as div_pay_dt,div_remark,Bank_Name,div_bank_acc_no,div_debit_amt,div_new_icno from mem_member AS mm Left join mem_divident AS md ON md.div_new_icno = mm.mem_new_icno left join Ref_Nama_Bank as bn ON md.div_bank_cd=bn.Bank_Code and md.Acc_sts ='Y' where md.div_approve_ind='SA' and mm.Acc_sts ='Y' and md.div_new_icno='" + Txtnokp.Text + "' and year(md.div_pay_dt) <='" + DropDownList4.SelectedItem.Text + "') d on d.div_new_icno=a.mem_new_icno FULL OUTER JOIN (select Convert(char(10),ast_end_date,105) as ast_end_date,ast_st_balance_amt,ast_monthly_collect_amt,ast_new_icno from aim_st ) e on e.ast_new_icno=a.mem_new_icno) where a.mem_new_icno='" + Txtnokp.Text + "' order by sha_approve_Dt desc");

                    if(DropDownList4.SelectedValue != "")
                    {
                        ss1 = DropDownList4.SelectedItem.Text;
                    }

                    if (ddlnegri.SelectedValue != "")
                    {
                        ss2 = ddlnegri.SelectedItem.Text;
                    }


                    RptviwerStudent.Reset();
                     ds.Tables.Add(dt);

                     RptviwerStudent.LocalReport.DataSources.Clear();

                     //ReportDataSource RDS1 = new ReportDataSource("BANKSLIP", dt1);
                     //ReportViewer1.ProcessingMode = ProcessingMode.Local;

                     RptviwerStudent.LocalReport.ReportPath = "keanggotan/Report2.rdlc";
                     //ReportViewer1.LocalReport.DataSources.Add(RDS1);
                     ReportDataSource rds = new ReportDataSource("list", dt);
                    ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("pyear", ss1),
                     new ReportParameter("negri", ss2),
                     new ReportParameter("pscd", TextBox3.Text),
                     new ReportParameter("app_dt", dt.Rows[0]["fee_approval_dt"].ToString())
                     };


                    RptviwerStudent.LocalReport.SetParameters(rptParams);
                    RptviwerStudent.LocalReport.DataSources.Add(rds);


                     //Refresh
                     RptviwerStudent.LocalReport.Refresh();
                     Warning[] warnings;

                     string[] streamids;

                     string mimeType;

                     string encoding;

                     string extension;

                     string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                            "  <PageWidth>12.20in</PageWidth>" +
                             "  <PageHeight>8.27in</PageHeight>" +
                             "  <MarginTop>0.1in</MarginTop>" +
                             "  <MarginLeft>0.5in</MarginLeft>" +
                              "  <MarginRight>0in</MarginRight>" +
                              "  <MarginBottom>0in</MarginBottom>" +
                            "</DeviceInfo>";

                     byte[] bytes = RptviwerStudent.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                     Response.Buffer = true;

                     Response.Clear();

                     Response.ContentType = mimeType;

                     Response.AddHeader("content-disposition", "attachment; filename=myfile." + extension);

                     Response.BinaryWrite(bytes);

                     //Response.Write("<script>");
                     //Response.Write("window.open('', '_newtab');");
                     //Response.Write("</script>");
                     Response.Flush();

                     Response.End();
                 }
                 else
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sila Pilih Tahun');", true);
                 }
             }
             else
             {
                 ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Medan Input Adalah Mandatori');", true);
             }
         }
         catch (Exception ex)
         {
             throw ex;
         }
     }

void clear()
{
    
}
protected void btn_reset_Click(object sender, EventArgs e)
{
    Response.Redirect("cetak_pt.aspx");
}

void instaudt()
{
    string audcd = "010801";
    string auddec = "CETAK PENAYARA TAHUNAN";
    string usrid = Session["New"].ToString();
    string curdt = DateTime.Now.ToString();
    string Inssql = "insert into cmn_audit_trail(aud_crt_id,aud_crt_dt,aud_txn_cd,aud_txn_desc) values ('" + usrid + "','" + curdt + "','" + audcd + "','" + auddec + "')";
    Status = DBCon.Ora_Execute_CommamdText(Inssql);
}
}