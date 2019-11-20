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

public partial class KW_inv_stock_details : System.Web.UI.Page
{
    DBConnection Dbcon = new DBConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                //string nameshbt = Session["resitno"].ToString();
                //if (nameshbt != "")
                //{
                    DataTable dt = new DataTable();
                    dt = Dbcon.Ora_Execute_table("select s1.id,s1.kod_barang,s1.jenis_barang,s1.keterangan,FORMAT(s1.do_tarikh,'dd/MM/yyyy', 'en-us') as do_tarikh,s1.kuantiti,s1.unit,s1.gst_amaun,s1.gst_amaun_jum,s1.baki_kuantiti,s1.jumlah,(cast(s1.kuantiti as int) - cast(s1.baki_kuantiti as int)) as out_qty,(s1.unit * (cast(s1.baki_kuantiti as int))) as jum_kes from KW_kemasukan_inventori s1 order by s1.kod_barang,s1.do_tarikh Asc");

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

                ReportDataSource rds = new ReportDataSource("kwinvstk", dt);

                ReportViewer1.LocalReport.DataSources.Add(rds);
           
                //Path
                ReportViewer1.LocalReport.ReportPath = "kewengan/KW_inventori_stok.rdlc";
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
                //}
            }
            catch (Exception ex)
            {
                txtError.Text = ex.ToString();

            }

        }
    }
}