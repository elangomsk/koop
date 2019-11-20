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

public partial class KW_Penerimaan_credit_rptview : System.Web.UI.Page
{
    DBConnection Dbcon = new DBConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                string nameshbt = Session["dbtno"].ToString();
                if (nameshbt != "")
                {
                    DataTable dt = new DataTable();
                    dt = Dbcon.Ora_Execute_table("select distinct no_invois,untuk_akaun,ss1.nama_akaun as nama_syarikat,ISNULL(ss2.Ref_alamat,'') as Ref_alamat,ISNULL(ss2.ref_alamat_ked,'') ref_alamat_ked,ISNULL(ss2.pem_bandar,'') pem_bandar,ISNULL(n1.hr_negeri_desc,'') hr_negeri_desc,ISNULL(ss2.pem_poskod,'') pem_poskod,ISNULL(c1.CountryName,'') CountryName,keterangan,no_Rujukan,jumlah,FORMAT(ISNULL(tarikh_credit,''),'dd/MM/yyyy', 'en-us') as tarikh_credit from KW_Penerimaan_Credit_item left join KW_Ref_Carta_Akaun ss1 on ss1.kod_akaun=nama_pelanggan left join KW_Ref_Pembekal ss2 on ss2.Ref_kod_akaun=ss1.kod_akaun left join Country c1 on c1.ID=ss2.pem_negera left join Ref_hr_negeri n1 on n1.hr_negeri_Code=ss2.pem_negeri where no_Rujukan='" + nameshbt + "' ");

                    rptcredit.Reset();

                    List<DataRow> listResult = dt.AsEnumerable().ToList();
                    listResult.Count();
                    int countRow = 0;
                    countRow = listResult.Count();

                    if (countRow != 0)
                    {
                        txtError.Text = "";
                        //Display Report
                        rptcredit.LocalReport.DataSources.Clear();

                        ReportDataSource rds = new ReportDataSource("kwkredit", dt);

                        rptcredit.LocalReport.DataSources.Add(rds);

                        //Path
                        rptcredit.LocalReport.ReportPath = "kewengan/KW_Kredit_Note.rdlc";
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


                        rptcredit.LocalReport.SetParameters(rptParams);
                        //ReportViewer1.LocalReport.ExecuteReportInCurrentAppDomain(AppDomain.CurrentDomain.Evidence);
                        //Refresh
                        rptcredit.LocalReport.Refresh();

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

                        byte[] bytes = rptcredit.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);


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