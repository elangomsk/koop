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

public partial class KW_Pembeyaran_invbil_rptview : System.Web.UI.Page
{
    DBConnection Dbcon = new DBConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                string nameshbt = Session["pvno"].ToString();
                if (nameshbt != "")
                {
                    DataTable dt = new DataTable();
                    dt = Dbcon.Ora_Execute_table("select m1.Pv_no ss1,FORMAT(m1.tarkih_pv,'dd/MM/yyyy', 'en-us')  ss2,m1.untuk_akaun ss3,s1.nama_syarikat ss4,s1.alamat_syarikat ss5,s2.Ref_nama_syarikat ss6,s2.Ref_gst_id ss7,FORMAT(m1.tarikh_invois,'dd/MM/yyyy', 'en-us') ss8,s3.keterangan ss9,s3.quantiti ss10,s3.othgstjumlah ss11,s3.gstjumlah ss12,s3.Overall ss13,m1.payamt ss14,s4.Ref_No_akaun ss15,m1.No_cek ss16,s3.unit ss17,s4.Ref_nama_bank ss18 from KW_Pembayaran_Pay_voucer m1 left join KW_Profile_syarikat s1 on s1.kod_syarikat=m1.untuk_akaun left join KW_Ref_Pembekal s2 on s2.Ref_kod_akaun=m1.Deb_kod_akaun left join KW_Pembayaran_invoisBil_item s3 on s3.no_invois=m1.no_invois left join KW_Ref_Akaun_bank s4 on s4.Ref_kod_akaun=m1.Cre_kod_akaun where m1.Pv_no='"+ nameshbt + "'");

                    ReportViewer1.Reset();

                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    if (countRow != 0)
                    {
                        txtError.Text = "";
                        //Display Report
                        ReportViewer1.LocalReport.DataSources.Clear();

                        ReportDataSource rds = new ReportDataSource("kwpv", dt);

                        ReportViewer1.LocalReport.DataSources.Add(rds);

                        //Path
                        ReportViewer1.LocalReport.ReportPath = "kewengan/KW_Pembayaran_PV.rdlc";
                        string branch;
                        //ToDate.Text = today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);


                        //Parameters
                        ReportParameter[] rptParams = new ReportParameter[]{
                     //new ReportParameter("fromDate",FromDate .Text ),
                     //new ReportParameter("toDate",ToDate .Text )
                     //new ReportParameter("fromDate",datedari  ),
                     //new ReportParameter("toDate",datehingga ),
                     //     new ReportParameter("caw",branch ),     
                     //       new ReportParameter("Cdate",DateTime.Now.ToString("dd/MM/yyyy") ),     
                     //new ReportParameter("Date",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")   )
                     };


                        ReportViewer1.LocalReport.SetParameters(rptParams);
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
                        Response.AddHeader("content-disposition", "inline; filename=myfile." + extension);
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