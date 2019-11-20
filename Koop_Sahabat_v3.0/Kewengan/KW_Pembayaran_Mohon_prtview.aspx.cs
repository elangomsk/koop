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
using System.Collections;
using System.Web.SessionState;
using System.Threading;
using System.Reflection;

public partial class KW_Pembayaran_Mohon_prtview : System.Web.UI.Page
{
    DBConnection Dbcon = new DBConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
               
                string nameshbt1 = Session["permohon_no"].ToString();
                if (nameshbt1 != "")
                {
                    DataTable dt = new DataTable();
                    dt = Dbcon.Ora_Execute_table("	 select * from ( "
                        + " select mhn_no_permohonan as mohon_no, perkara main_ket, mhn_keterangan as ket, mhn_amount_bsr as amt, b.v1 as nama, s1.Bank_Name as nama_bank, mhn_noacc_bank as acc_no "
                        + " , a.no_ruj as ruj,case when Jenis_permohonan = '01' then 'Tender' when Jenis_permohonan = '02' then 'Sebut Harga' when Jenis_permohonan = '03' then 'Pembelian Terus' when Jenis_permohonan = '04' then 'Panjar Wang Runcit' when Jenis_permohonan = '05' then 'Pembelian Secara Darurat/Kecemasan' when Jenis_permohonan = '06' then 'Tuntutan Perubatan' else 'Lain-lain' end as perol "
                        + " , s2.Jenis_bayaran as pem, Format(a.tarkih_permohonan, 'dd/MM/yyyy') tarkih, s3.Kk_username crt_name, Format(a.cr_dt, 'dd/MM/yyyy') crt_dt  from KW_Pembayaran_mohon_item m1 "
                        + " outer apply(select * from KW_Pembayaran_mohon where no_permohonan = m1.mhn_no_permohonan) as a "
                        + " outer apply( select stf_name v1 from hr_staff_profile where stf_staff_no=m1.mhn_byr_kepada union all select Ref_nama_syarikat v1 from KW_Ref_Pembekal where Ref_no_syarikat=m1.mhn_byr_kepada  union all select Ref_nama_syarikat v1 from KW_Ref_Pelanggan where Ref_no_syarikat=m1.mhn_byr_kepada ) as b "
                        + " left join Ref_Nama_Bank s1 on s1.Bank_Code = mhn_bank left join KW_Jenis_Cara_bayaran s2 on s2.Jenis_bayaran_cd = a.cara_bayaran left join kk_user_login s3 on s3.kk_userid = a.crt_id "
                        + "where mhn_no_permohonan = '"+ nameshbt1 + "') as a1 ");

                    ReportViewer1.Reset();

                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    if (countRow != 0)
                    {
                        DataTable get_pfl = new DataTable();
                        get_pfl = Dbcon.Ora_Execute_table("select syar_logo from KW_Profile_syarikat where cur_sts='1' and Status='A'");

                        string imagePath = string.Empty;
                        if (get_pfl.Rows[0]["syar_logo"].ToString() != "")
                        {
                            imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/" + get_pfl.Rows[0]["syar_logo"].ToString() + "")).AbsoluteUri;

                        }
                        else
                        {
                            imagePath = new Uri(Server.MapPath("~/FILES/Profile_syarikat/user.png")).AbsoluteUri;
                        }

                        txtError.Text = "";
                        //Display Report
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.EnableExternalImages = true;
                        ReportDataSource rds = new ReportDataSource("Mohon", dt);

                        ReportViewer1.LocalReport.DataSources.Add(rds);

                        //Path
                        ReportViewer1.LocalReport.ReportPath = "kewengan/KW_Pembayaran_Mohon.rdlc";
                        string branch;
                        //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);


                        //Parameters
                        ReportParameter[] rptParams = new ReportParameter[]{
                     new ReportParameter("v1",imagePath)
                     //new ReportParameter("toDate",ToDate .Text )
                     //new ReportParameter("fromDate",datedari  ),
                     //new ReportParameter("toDate",datehingga ),
                     //     new ReportParameter("caw",branch ),     
                     //       new ReportParameter("Cdate",DateTime.Now.ToString("dd/MM/yyyy") ),     
                     //new ReportParameter("Date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")   )
                     };


                        ReportViewer1.LocalReport.SetParameters(rptParams);
                        ReportViewer1.LocalReport.DisplayName = "PROSEDUR_PEMBAYARAN_" + DateTime.Now.ToString("yyyyMMdd");
                        //ReportViewer1.LocalReport.ExecuteReportInCurrentAppDomain(AppDomain.CurrentDomain.Evidence);
                        //Refresh
                        ReportViewer1.LocalReport.Refresh();

                        Warning[] warnings;

                        string[] streamids;

                        string mimeType;

                        string encoding;

                        string extension;

                        //string devinfo = "<DeviceInfo><ColorDepth>32</ColorDepth><DpiX>350</DpiX><DpiY>350</DpiY><OutputFormat>PDF</OutputFormat>" +
                        //       "  <PageWidth>12.20in</PageWidth>" +
                        //        "  <PageHeight>8.27in</PageHeight>" +
                        //        "  <MarginTop>0.1in</MarginTop>" +
                        //        "  <MarginLeft>0.5in</MarginLeft>" +
                        //         "  <MarginRight>0in</MarginRight>" +
                        //         "  <MarginBottom>0in</MarginBottom>" +
                        //       "</DeviceInfo>";

                        byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        Response.AddHeader("content-disposition", "inline; filename=PROSEDUR_PEMBAYARAN_"+ nameshbt1 + "." + extension);
                        Response.BinaryWrite(bytes);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                txtError.Text = ex.ToString();

            }

        }
    }
}