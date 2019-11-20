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

public partial class KW_Penerimaan_resit_Rptview : System.Web.UI.Page
{
    DBConnection Dbcon = new DBConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                string nameshbt = Session["resitno"].ToString();
                if (nameshbt != "")
                {
                    DataTable dt = new DataTable();
                    dt = Dbcon.Ora_Execute_table("select a.no_resit,b.res_rujukan,b.res_keterangan,b.Overall,ss1.nama_syarikat,ss2.nama_akaun,FORMAT(a.tarikh_resit,'dd/MM/yyyy', 'en-us') tarikh_resit from (select * from KW_Penerimaan_resit where no_resit='"+ nameshbt + "') as a left join (select *  from kw_penerimaan_resit_item m1) as b on b.no_resit=a.no_resit left join KW_Profile_syarikat ss1 on ss1.kod_syarikat=a.untuk_akaun left join KW_Ref_Carta_Akaun ss2 on ss2.kod_akaun=a.nama_pelanggan");

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

                ReportDataSource rds = new ReportDataSource("kwresit", dt);

                ReportViewer1.LocalReport.DataSources.Add(rds);
           
                //Path
                ReportViewer1.LocalReport.ReportPath = "kewengan/KW_Penerimaan_Resit.rdlc";
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