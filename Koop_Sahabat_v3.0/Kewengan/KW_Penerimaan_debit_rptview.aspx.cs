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

public partial class KW_Penerimaan_debit_rptview : System.Web.UI.Page
{
    DBConnection Dbcon = new DBConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                string nameshbt = Session["dbtno1"].ToString();
                if (nameshbt != "")
                {
                    DataTable dt = new DataTable();
                    dt = Dbcon.Ora_Execute_table("select no_invois,untuk_akaun,ss1.nama_syarikat,ss1.alamat_syarikat,keterangan,no_Rujukan,jumlah,FORMAT(ISNULL(tarikh_credit,''),'dd/MM/yyyy', 'en-us') as tarikh_credit from KW_Penerimaan_Debit_item left join KW_Profile_syarikat ss1 on ss1.kod_syarikat=untuk_akaun where no_Rujukan='" + nameshbt + "' ");

                    rptdebit.Reset();

                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    if (countRow != 0)
                    {
                        txtError.Text = "";
                        //Display Report
                        rptdebit.LocalReport.DataSources.Clear();

                        ReportDataSource rds = new ReportDataSource("kwdebit", dt);

                        rptdebit.LocalReport.DataSources.Add(rds);

                        //Path
                        rptdebit.LocalReport.ReportPath = "kewengan/KW_Debit_Note.rdlc";
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


                        rptdebit.LocalReport.SetParameters(rptParams);
                        //ReportViewer1.LocalReport.ExecuteReportInCurrentAppDomain(AppDomain.CurrentDomain.Evidence);
                        //Refresh
                        rptdebit.LocalReport.Refresh();

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

                        byte[] bytes = rptdebit.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


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