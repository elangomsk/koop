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

public partial class pp_jkkpa_batch : System.Web.UI.Page
{
    DBConnection DBCon = new DBConnection();
    string conString = ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString;
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KSAIMBConnectionString"].ConnectionString);   
    StudentWebService service = new StudentWebService();    
    DataSet ds = new DataSet();
    string level, userid;    
    string Status = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["New"] != null)
            {
                userid = Session["New"].ToString();
                //Txtfromdate.Attributes.Add("readonly", "readonly");
                //Txttodate.Attributes.Add("readonly", "readonly");
                txtkel.Attributes.Add("readonly", "readonly");
                             
            }
            else
            {
                Response.Redirect("../KSAIMB_Login.aspx");
            }
        }
    }


    public void showreport()
    {

        try
        {
          
                
                string t1 = Txtfromdate.Text;
                string t2 = Txttodate.Text;
                DateTime ft = DateTime.ParseExact(t1, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                string fdate = ft.ToString("yyyy-mm-dd");
                DateTime td = DateTime.ParseExact(t2, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                string tdate = td.ToString("yyyy-mm-dd");
                //Path
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = DBCon.Ora_Execute_table("select app_applcn_no,JA.app_new_icno,JA.app_age,JA.app_name,rt.tujuan_desc,JA.app_apply_amt,JA.app_apply_dur,JJA.jkk_approve_amt,JJA.jkk_approve_dur,cal_stamp_duty_amt,cal_process_fee,cal_deposit_amt,cal_installment_amt, cal_tkh_amt, cal_credit_fee, cal_profit_amt from jpa_application as JA LEFT join jpa_calculate_fee cf on cf.cal_applcn_no = JA.app_applcn_no Left join ref_tujuan as rt on rt.tujuan_cd=JA.app_loan_purpose_cd Inner join jpa_jkkpa_approval as JJA ON JJA.jkk_applcn_no = JA.app_applcn_no where JJA.jkk_meeting_dt>=DATEADD(day, DATEDIFF(day, 0, '"+ fdate +"'), 0) and JJA.jkk_meeting_dt<=DATEADD(day, DATEDIFF(day, 0, '"+ tdate + "'), +1) and ISNULL(JJA.jkk_result_ind,'') =''");

                Reportviwer.Reset();
                ds.Tables.Add(dt);
                Reportviwer.LocalReport.DataSources.Clear();

               

                if (dt.Rows.Count != 0)
                {
                txtkel.Text = "PA" + DateTime.Now.ToString("yyyyMMdd");
                for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Inssql = "Update jpa_jkkpa_approval set pa_batch_no='" + txtkel.Text + "',jkk_upd_id='" + Session["New"].ToString() + "' ,jkk_upd_dt='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where jkk_applcn_no='"+ dt.Rows[i]["app_applcn_no"].ToString() + "' and ISNULL(jkk_result_ind,'') =''";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql);

                        string Inssql1 = "Update jpa_application set jpa_batch_no='" + txtkel.Text + "' where app_applcn_no='" + dt.Rows[i]["app_applcn_no"].ToString() + "'";
                        Status = DBCon.Ora_Execute_CommamdText(Inssql1);
                    }

                    if (Status == "SUCCESS")
                    {
                        Reportviwer.LocalReport.ReportPath = "PELABURAN_ANGGOTA/jkkpa_batch.rdlc";
                        ReportDataSource rds = new ReportDataSource("jkbatch", dt);
                        ReportParameter[] rptParams = new ReportParameter[]{
                      new ReportParameter("fromDate", Txtfromdate.Text),
                     new ReportParameter("toDate", Txttodate.Text),
                     new ReportParameter("Kelompok", txtkel.Text),
                     new ReportParameter("current_date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))

                     };


                        Reportviwer.LocalReport.SetParameters(rptParams);
                        Reportviwer.LocalReport.DataSources.Add(rds);

                        //Refresh
                        Reportviwer.LocalReport.Refresh();
                       
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Rekod Tidak Dijumpai.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
                }
          
        }
        catch (Exception ex)
        {
            throw ex;
            //Request.Redirect(url, false);
        }

    }




   

    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Txtfromdate.Text != "" && Txttodate.Text != "")
        {
                showreport();
        }

        else
        {            
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "$.Zebra_Dialog('Medan Input Adalah Mandatori.',{'type': 'warning','title': 'Warning','auto_close': 2000});", true);
        }
    }
}