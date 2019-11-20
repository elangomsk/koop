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

public partial class KW_Penerimaan_invois_Rptview : System.Web.UI.Page
{
    DBConnection Dbcon = new DBConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                string nameshbt = Session["Invoisno"].ToString();
                if (nameshbt != "")
                {
                    DataTable dt = new DataTable();
                    dt = Dbcon.Ora_Execute_table("select untuk_akaun,nama_pelanggan,FORMAT(ISNULL(tarikh_invois,''),'dd/MM/yyyy', 'en-us') tarikh_invois,project_kod,kod_akauan,item,keterangan,cast(unit as money) unit,quantiti,discount,jumlah,gstjumlah,othgstjumlah,cast(Overall as money) Overall,no_invois,Ref_nama_syarikat as nama_syarikat,ref_alamat as a1,ref_alamat_ked as a2,pel_bandar as a3, n1.Decription as a4,pel_poskod as a5,cc1.CountryName as a6,Ref_no_fax as syar_nombo_fax,Ref_no_telefone as syar_nombo_teleph,tt.Ref_nama_cukai  from KW_Penerimaan_invois_item I Inner join KW_Ref_Pelanggan S on S.Ref_kod_akaun=I.nama_pelanggan left join Ref_Negeri as n1 on n1.Decription_Code=s.pel_negeri left join Country cc1 on cc1.ID=s.pel_negera left join KW_Ref_Tetapan_cukai tt on tt.Ref_kod_cukai=I.gsttype where no_invois='" + nameshbt + "' ");

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

                ReportDataSource rds = new ReportDataSource("INV", dt);

                ReportViewer1.LocalReport.DataSources.Add(rds);
           
                //Path
                ReportViewer1.LocalReport.ReportPath = "kewengan/KW_Penerimaan_Invoice.rdlc";
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